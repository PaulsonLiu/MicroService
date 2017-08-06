using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
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

        /// <summary>
        /// 获取配置节点对象
        /// </summary>
        public static T GetSetting<T>(string key, string fileName = "appsettings.json") where T : class, new()
        {
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .Add(new JsonConfigurationSource { Path = fileName, Optional = false, ReloadOnChange = true })
            .Build();
            var appconfig = new ServiceCollection()
            .AddOptions()
            .Configure<T>(config.GetSection(key))
            .BuildServiceProvider()
            .GetService<IOptions<T>>()
            .Value;
            return appconfig;
        }

        /// <summary>
        /// 设置并获取配置节点对象
        /// </summary>
        public static T SetConfig<T>(string key, Action<T> action, string fileName = "appsettings.json") where T : class, new()
        {
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(fileName, optional: true, reloadOnChange: true)
            .Build();
            var appconfig = new ServiceCollection()
            .AddOptions()
            .Configure<T>(config.GetSection(key))
            .Configure<T>(action)
            .BuildServiceProvider()
            .GetService<IOptions<T>>()
            .Value;
            return appconfig;
        }

    }
}
