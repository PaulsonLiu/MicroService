﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using MicroService.Common;
using Microsoft.EntityFrameworkCore;

namespace LifeService
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddMvc();

            //添加系统配置
            services.Configure<AppConfiguration>(Configuration.GetSection(nameof(AppConfiguration)));

            //添加跨域配置
            //services.AddCors(options => options.AddPolicy("AllowA", p => p.WithOrigins("http://a.example.com", "http://c.example.com").AllowAnyMethod().AllowAnyHeader()));
            services.AddCors(options => options.AddPolicy("Any", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            //添加sqlite数据库
            //services.AddDbContext<DataContext>(options => options.UseSqlite(Configuration.GetConnectionString("SqliteFilePath")));

            /* 说明：增加Sql数据库,使用vsCode运行命令：
             * 1.添加到数据：dotnet ef migrations add MyFirstMigration，
             * 2.执行更新：dotnet ef database update，
             * 删除之前记录：dotnet ef migrations remove
             */
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseMvc();
            app.UseCors("Any");
            //app.UseCors("AllowA");

            //nlog
            loggerFactory.AddNLog();
            loggerFactory.ConfigureNLog($"{env.ContentRootPath}\\nlog.config");
        }
    }
}
