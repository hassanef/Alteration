    using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Alteration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace IntegrationTest.ApiTest
{
    public class AlterationSenarioBase
    {
        private const string ApiUrlBase = "api/alteration";

        public TestServer CreateServer()
        {
            var webHostBuilder = new WebHostBuilder()
              .ConfigureAppConfiguration((builderContext, config) =>
               {
                   config.AddJsonFile("appsettings.json", optional: false);
               })
                .UseEnvironment("Development")
                .UseStartup<AlterationTestsStartup>();

            return new TestServer(webHostBuilder);
        }

        public static class Get
        {
            public static string GetAlteration(byte status)
            {
                return $"{ApiUrlBase}/Get/" + status;
            }
        }
     

        public static class Post
        {
            public static string AddAlteration = $"{ApiUrlBase}/Add";
            public static string ChangeStatus = $"{ApiUrlBase}/ChangeStatus";
        }
    }
}
