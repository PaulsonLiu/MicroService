using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroService.Common.Codes
{
    public enum ErrorCode
    {
        WrongKey = 10001,   //错误的请求KEY
        NoPermission = 10002,   //该KEY无请求权限
        ExpireKey = 10003,   //KEY过期
        WrongOpenId = 10004,   //错误的OPENID
        AppAuditTimeOut = 10005,   //应用未审核超时，请提交认证
        UnknowRequester = 10007,   //未知的请求源
        ForbiddenIP = 10008,   //被禁止的IP
        ForbiddenKey = 10009,   //被禁止的KEY
        OverLimitIP = 10011,   //当前IP请求超过限制
        OverLimitTime = 10012,   //请求超过次数限制
        OverLimitTestKey = 10013,   //测试KEY超过请求限制
        InnerError = 10014,   //系统内部异常(调用充值类业务时，请务必联系客服或通过订单查询接口检测订单，避免造成损失)
        ApiRepair = 10020,   //接口维护
        ApiDisable = 10021,   //接口停用
    }


}
