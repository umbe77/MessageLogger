using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MessageLogging.Models;

namespace MessageLogging.Controllers
{
    public class HelloController : ApiController
    {
        [HttpGet]
        public HelloResponse Hello()
        {
            return new HelloResponse
            {
                Message = "Hello World"
            };
        }

        [HttpPut]
        public HelloResponse Hello([FromBody] HelloRequest req)
        {
            return new HelloResponse
            {
                Message = $"{req.Message} {req.Name}!!!"
            };
        }
    }
}
