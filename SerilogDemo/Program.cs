﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Serilog;

namespace SerilogDemo {
    public class Program {
        public static void Main(string[] args) {

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                //.WriteTo.Seq("http://localhost:8081")
                .CreateLogger();
            try {
                Log.Information("Application Starting Up");
                CreateWebHostBuilder(args).Build().Run();
            } catch (Exception ex) {
                Log.Fatal(ex, "the application failed to start correctly.");
                //throw;
            }   
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) {
            
            return WebHost.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseStartup<Startup>();
        }
    }
}
