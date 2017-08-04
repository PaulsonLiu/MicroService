using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LifeService.Common
{
    public class AppConfigurtaionServices
    {
        private readonly IOptions<AppConfiguration> _appConfiguration;
        public AppConfigurtaionServices(IOptions<AppConfiguration> appConfiguration)
        {
            _appConfiguration = appConfiguration;
        }

        public AppConfiguration AppConfigurations
        {
            get
            {
                return _appConfiguration.Value;
            }
        }
    }
}
