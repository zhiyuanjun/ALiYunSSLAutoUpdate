// See https://aka.ms/new-console-template for more information

using AlibabaCloud.SDK.Cas20200407.Models;
using AliYunHeathCheck.AliYun;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.UserSecrets;

Console.WriteLine("Hello, World!");
// 创建配置构建器
var builder = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsetting.json", optional: true, reloadOnChange: true);

IConfiguration configuration = builder.Build();

var sslSetting = configuration.GetSection("ALiYunSSLSetting").Get<ALiYunSSLSetting>();

AliYunSSLApi api = new AliYunSSLApi(sslSetting);
//获取检测账号下全部证书
var Certificatelist = api.ListUserCertificateOrder();
Console.WriteLine($"检测到账号下共有{Certificatelist.Body.CertificateOrderList.Count}个证书");
//检测是否存在临近过期证书
foreach (var item in Certificatelist.Body.CertificateOrderList)
{
    if (DateTime.Parse(item.EndDate) < DateTime.Now.AddDays(3))
    {
        //自动申请新的证书
        CreateNewCertificate(item).Wait();
    }
    else
    {
        Console.WriteLine($"域名{item.CommonName}的证书未临近过期，预计过期时间：{item.EndDate},当前时间：{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
    }
}

return;


async Task CreateNewCertificate(
            ListUserCertificateOrderResponseBody.ListUserCertificateOrderResponseBodyCertificateOrderList old)
        {
      
            Console.WriteLine($"域名{old.CommonName}的证书即将过期，先启动自动更新流程");
            AliYunSSLApi api = new AliYunSSLApi(sslSetting);

            string csr = "";
            if (File.Exists(sslSetting.pempath+$"{old.CommonName}.key"))
            {
                csr = await File.ReadAllTextAsync(sslSetting.pempath + $"{old.CommonName}.key");
            }
            else
            {
                csr = api.CreateCsr(old).Body.Csr;
            }
 
            Console.WriteLine($"域名{old.CommonName} 生成CSR成功，开始申请新证书");
            var order = api.CreateCertificateWithCsrRequest(csr);
            Console.WriteLine($"域名{old.CommonName} 申请新证书成功，OrderId:{order.Body.OrderId} 开始审核");
            bool isend = false;
            while (!isend)
            {
                await Task.Delay(1000 * 60);
                var ret = api.DescribeCertificateState(order.Body.OrderId??0);
                
                switch (ret.Body.Type)
                {
                    case "domain_verify":
                        Console.WriteLine($"域名{old.CommonName}的证书需要手动验证，请及时处理。");
                        break;
                    case "process":
                        Console.WriteLine($"域名{old.CommonName}的证书审核中。");
                        break;
                    case "verify_fail":
                        Console.WriteLine($"域名{old.CommonName}的证书审核失败，请及时处理。");
                        isend = true;
                        break;
                    case "certificate":
                        Console.WriteLine($"域名{old.CommonName}的证书已签发。");
                        //写key
                        Console.WriteLine($"域名{old.CommonName} 开始写入{old.CommonName}.key文件");
                        WriteFile($"{old.CommonName}.key",csr);
                        //写pem
                        Console.WriteLine($"域名{old.CommonName} 开始写入{old.CommonName}.pem文件");
                        WriteFile($"{old.CommonName}.pem",ret.Body.Certificate);
                        Console.WriteLine($"域名{old.CommonName}的证书更新完成。");
                        isend = true;
                        break;
                    default:
                        break;
                }
            }
          
        }
        
    void WriteFile(string filePath,string content)
        {
            try
            {
                File.WriteAllText(sslSetting.pempath+filePath, content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"写入文件时发生错误: {ex.Message}");
            }
        }