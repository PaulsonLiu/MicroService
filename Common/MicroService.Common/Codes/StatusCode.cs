using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroService.Common.Codes
{
    public enum StatusCode
    {
        Success = 200, //请求(或处理)成功
        Info = 500, //检查逻辑不通过
        Error = 501, //内部请求出错
        Unauthorized = 401,//未授权标识
        ParameterError = 400,//请求参数不完整或不正确
        TokenInvalid = 403,//请求TOKEN失效
        HttpMehtodError = 405,//HTTP请求类型不合法
        HttpRequestError = 406,//HTTP请求不合法
        URLExpireError = 407,//HTTP请求不合法
    }

}
