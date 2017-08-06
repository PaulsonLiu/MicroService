using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MicroService.Common;

//log level : Trace -> Debug-> Information -> Warning-> Error-> Critical

namespace LifeService.Controllers
{
    [Route("api/[controller]")]
    public class LoggerController : BaseController<LoggerController>
    {
        private readonly ILogger<LoggerController> _logger;
        public LoggerController(ILogger<LoggerController> logger)
        {
            _logger = logger;
        }

        public override IEnumerable<string> Get()
        {
            //控制台输出
            _logger.LogInformation("提示信息");
            _logger.LogWarning("警告信息");
            _logger.LogError("错误信息");

            //文件输出
            Logger.Info("Log info");
            Logger.Error("system error");

            return base.Get();
        }

    }
}
