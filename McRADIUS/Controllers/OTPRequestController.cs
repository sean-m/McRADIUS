using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using McRADIUS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace McRADIUS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OTPRequestController : ControllerBase
    {
        private readonly OTPDbContext ctx;

        public OTPRequestController(OTPDbContext Context)
        {
            ctx = Context;
        }

        // GET: api/OTPRequest
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/OTPRequest/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {

            return "Foo";
        }

        // POST: api/OTPRequest
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/OTPRequest/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
