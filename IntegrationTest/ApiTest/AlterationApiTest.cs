using Alteration;
using Application.Commands;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest.ApiTest
{
    public class AlterationApiTest:AlterationSenarioBase
    {

        [Fact]
        public async Task Post_add_and_response_ok_status_code()
        {
            using (var server = CreateServer())
            {
                var content = new StringContent(BuildOrder(), UTF8Encoding.UTF8, "application/json");
                var response = await server.CreateClient()
                   .PostAsync(Post.AddAlteration, content);

                response.EnsureSuccessStatusCode();
            }
        }
        [Fact]
        public async Task Get_alteration_and_response_ok_status_code()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                   .GetAsync(Get.GetAlteration(0));

                response.EnsureSuccessStatusCode();
            }
        }
        //[Fact]
        //public async Task Post_change_status_and_response_ok_status_code()
        //{
        //    using (var server = CreateServer())
        //    {
        //        var content = new StringContent(BuildChangeStatus(), UTF8Encoding.UTF8, "application/json");
        //        var response = await server.CreateClient()
        //           .PostAsync(Post.ChangeStatus, content);

        //        response.EnsureSuccessStatusCode();
        //    }
        //}
        string BuildOrder()
        {
            var order = new CreateOrderAlterationCommand(3, -1, 0, 5, "testCustomer");
            return JsonConvert.SerializeObject(order);
        }
        //string BuildChangeStatus()
        //{
        //    var order = new ChangeStatusOrderAlterationCommend(2, 3);
        //    return JsonConvert.SerializeObject(order);
        //}
    }
}
