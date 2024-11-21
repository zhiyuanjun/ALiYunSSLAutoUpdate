using AlibabaCloud.SDK.Cas20200407;
using AlibabaCloud.SDK.Cas20200407.Models;
using Tea;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AliYunHeathCheck.AliYun
{
    public class AliYunSSLApi
    {
        public AliYunSSLApi(ALiYunSSLSetting _setting)
        {
            setting = _setting;
        }

        private ALiYunSSLSetting setting { get; set; }
 

        private Client CreateClient()
        {
            AlibabaCloud.OpenApiClient.Models.Config config = new AlibabaCloud.OpenApiClient.Models.Config()
            {
                //取环境变量 ALIBABA_CLOUD_ACCESS_KEY_ID 和 ALIBABA_CLOUD_ACCESS_KEY_SECRET。
                // 必填，您的 AccessKey ID
                AccessKeyId = Environment.GetEnvironmentVariable("ALIBABA_CLOUD_ACCESS_KEY_ID", EnvironmentVariableTarget.Machine),
                // 必填，您的 AccessKey Secret
                AccessKeySecret = Environment.GetEnvironmentVariable("ALIBABA_CLOUD_ACCESS_KEY_SECRET", EnvironmentVariableTarget.Machine),
                
            };
            config.Endpoint = "cas.aliyuncs.com";
            return new AlibabaCloud.SDK.Cas20200407.Client(config);
        }

        public ListUserCertificateOrderResponse ListUserCertificateOrder()
        {
            AlibabaCloud.SDK.Cas20200407.Client client = CreateClient();
            AlibabaCloud.SDK.Cas20200407.Models.ListUserCertificateOrderRequest listUserCertificateOrderRequest = new AlibabaCloud.SDK.Cas20200407.Models.ListUserCertificateOrderRequest()
            {
                Status = "ISSUED",
                OrderType = "CERT",
            };
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                // 复制代码运行请自行打印 API 的返回值
                Console.WriteLine($"请求阿里云api ListUserCertificateOrder。");
                return client.ListUserCertificateOrderWithOptions(listUserCertificateOrderRequest, runtime);
            }
            catch (TeaException error)
            {
                // 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                // 错误 message
                Console.WriteLine(error.Message);
                // 诊断地址
                Console.WriteLine(error.Data["Recommend"]);
                AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message);
                
                Console.WriteLine($"请求阿里云api ListUserCertificateOrder失败：错误信息：{error.Message}");
            }
            catch (Exception _error)
            {
                TeaException error = new TeaException(new Dictionary<string, object>
                {
                    { "message", _error.Message }
                });
                // 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                // 错误 message
                Console.WriteLine(error.Message);
                // 诊断地址
                Console.WriteLine(error.Data["Recommend"]);
                AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message);
                
                Console.WriteLine($"请求阿里云api ListUserCertificateOrder失败：错误信息：{error.Message}");
            }

            return null;
        }

        public CreateCsrResponse CreateCsr(ListUserCertificateOrderResponseBody.ListUserCertificateOrderResponseBodyCertificateOrderList certificate)
        {
            AlibabaCloud.SDK.Cas20200407.Client client = CreateClient();
            AlibabaCloud.SDK.Cas20200407.Models.CreateCsrRequest createCsrRequest = new AlibabaCloud.SDK.Cas20200407.Models.CreateCsrRequest
            {
                KeySize = 2048,
                Algorithm = "RSA",
                CommonName = certificate.Domain,
                CountryCode = certificate.Country,
                Province = certificate.Province,
                Locality = certificate.City,
            };
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                // 复制代码运行请自行打印 API 的返回值
                Console.WriteLine($"请求阿里云api CreateCsr。");

                return client.CreateCsrWithOptions(createCsrRequest, runtime);
            }
            catch (TeaException error)
            {
                // 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                // 错误 message
                Console.WriteLine(error.Message);
                // 诊断地址
                Console.WriteLine(error.Data["Recommend"]);
                AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message);
                
                Console.WriteLine($"请求阿里云api CreateCsr 失败：错误信息：{error.Message}");
            }
            catch (Exception _error)
            {
                TeaException error = new TeaException(new Dictionary<string, object>
                {
                    { "message", _error.Message }
                });
                // 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                // 错误 message
                Console.WriteLine(error.Message);
                // 诊断地址
                Console.WriteLine(error.Data["Recommend"]);
                AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message);
                
                Console.WriteLine($"请求阿里云api CreateCsr 失败：错误信息：{error.Message}");
            }

            return null;
        }

        public CreateCertificateWithCsrRequestResponse CreateCertificateWithCsrRequest(string csr)
        {
            AlibabaCloud.SDK.Cas20200407.Client client = CreateClient();
            AlibabaCloud.SDK.Cas20200407.Models.CreateCertificateWithCsrRequestRequest createCertificateWithCsrRequestRequest = new AlibabaCloud.SDK.Cas20200407.Models.CreateCertificateWithCsrRequestRequest()
            {
                Csr = csr,
                ProductCode = setting.ProductCode,
                Username = setting.Username,
                Phone = setting.Phone,
                Email = setting.Email,
                ValidateType = setting.ValidateType,
            };
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                // 复制代码运行请自行打印 API 的返回值
                Console.WriteLine($"请求阿里云api CreateCertificateWithCsrRequest。");

                return client.CreateCertificateWithCsrRequestWithOptions(createCertificateWithCsrRequestRequest, runtime);
            }
            catch (TeaException error)
            {
                // 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                // 错误 message
                Console.WriteLine(error.Message);
                // 诊断地址
                Console.WriteLine(error.Data["Recommend"]);
                AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message);
                
                Console.WriteLine($"请求阿里云api CreateCertificateWithCsrRequest 失败：错误信息：{error.Message}");
            }
            catch (Exception _error)
            {
                TeaException error = new TeaException(new Dictionary<string, object>
                {
                    { "message", _error.Message }
                });
                // 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                // 错误 message
                Console.WriteLine(error.Message);
                // 诊断地址
                Console.WriteLine(error.Data["Recommend"]);
                AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message);
                
                Console.WriteLine($"请求阿里云api CreateCertificateWithCsrRequest 失败：错误信息：{error.Message}");
            }

            return null;
        }

        public DescribeCertificateStateResponse DescribeCertificateState(long OrderId)
        {
            AlibabaCloud.SDK.Cas20200407.Client client = CreateClient();
            AlibabaCloud.SDK.Cas20200407.Models.DescribeCertificateStateRequest describeCertificateStateRequest = new AlibabaCloud.SDK.Cas20200407.Models.DescribeCertificateStateRequest();
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                // 复制代码运行请自行打印 API 的返回值
                Console.WriteLine($"请求阿里云api DescribeCertificateState。");

                return client.DescribeCertificateStateWithOptions(describeCertificateStateRequest, runtime);
            }
            catch (TeaException error)
            {
                // 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                // 错误 message
                Console.WriteLine(error.Message);
                // 诊断地址
                Console.WriteLine(error.Data["Recommend"]);
                AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message);
                Console.WriteLine($"请求阿里云api DescribeCertificateState 失败：错误信息：{error.Message}");
            }
            catch (Exception _error)
            {
                TeaException error = new TeaException(new Dictionary<string, object>
                {
                    { "message", _error.Message }
                });
                // 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                // 错误 message
                Console.WriteLine(error.Message);
                // 诊断地址
                Console.WriteLine(error.Data["Recommend"]);
                AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message);
                Console.WriteLine($"请求阿里云api DescribeCertificateState 失败：错误信息：{error.Message}");
            }

            return null;
        }



    }
}
