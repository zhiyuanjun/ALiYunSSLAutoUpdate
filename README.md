# ALiYunSSLAutoUpdate
自动调用阿里云数字证书管理的api来对临期的证书进行申请更换

食用方法：

1：设置系统环境变量ALIBABA_CLOUD_ACCESS_KEY_ID 和 ALIBABA_CLOUD_ACCESS_KEY_SECRET。 用于授权访问阿里云的资源

2：在配置文件里面配置申请证书相关的参数以及证书的存储路径

3：搞个定时执行的工具，比如说任务计划程序按照需要的频率配置一下自动执行即可

以上
