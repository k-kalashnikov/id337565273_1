using System;
using System.Diagnostics;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SP.Contract.API.Extensions
{
    public static class AutoMapperStartupExtensions
    {
        public static void AutoMapperConfigure(this IWebHostEnvironment env, Assembly assembly)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(assembly);
            });

            if (env != null && env.IsDevelopment())
            {
                try
                {
                    config.AssertConfigurationIsValid();
                }
                catch (Exception e)
                {
                    Debugger.Break();
                    throw;
                }
            }
        }
    }
}
