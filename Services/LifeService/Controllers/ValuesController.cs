using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;

namespace LifeService.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : BaseController<ValuesController>
    {
        public override IEnumerable<string> Get()
        {
            string connnectionString = AppConfig.ConnectionString;

            return base.Get();
        }
    }
}
