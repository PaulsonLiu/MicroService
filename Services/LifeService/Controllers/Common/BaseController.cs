using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using MicroService.Common;
using LifeService.Common;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LifeService.Controllers
{
    [Route("api/[controller]")]
    public class BaseController<T> : Controller
    {
        public static Logger Logger = LogManager.GetCurrentClassLogger();
        public readonly AppConfiguration AppConfig;

        public BaseController()
        {
            AppConfig = AppConfigurtaionServices.GetSetting<AppConfiguration>(nameof(AppConfiguration));
        }
        // GET: api/values
        [HttpGet]
        public virtual IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public virtual string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public virtual void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public virtual void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public virtual void Delete(int id)
        {
        }
    }
}
