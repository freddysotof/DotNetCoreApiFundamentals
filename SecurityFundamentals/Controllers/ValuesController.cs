using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SecurityFundamentals.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SecurityFundamentals.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigins")]
    //[Authorize(AuthenticationSchemes =  JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    public class ValuesController : ControllerBase
    {
        private readonly IDataProtector _protector;
        private readonly IConfiguration configuration;
        private readonly HashService _hashService;

        public ValuesController(IDataProtectionProvider protectionProvider, IConfiguration configuration,HashService hashService)
        {
            this._protector = protectionProvider.CreateProtector("valor_unico");
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this._hashService = hashService;
        }

        [HttpGet("hash")]
        public ActionResult GetHash()
        {
            string plain = "Freddy Soto";
            var hashResult = _hashService.Hash(plain).Hash;
            var hashResult2 = _hashService.Hash(plain).Hash;
            return Ok(new { hashResult, hashResult2 });
        }

        // GET api/values
        [HttpGet]
        [ResponseCache(Duration =15)]

        public ActionResult<string> Get()
        {
            return DateTime.Now.Second.ToString();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            //var limitedTimeProtector = _protector.ToTimeLimitedDataProtector();
            //string plain = "Freddy Soto";
            //string cipher = limitedTimeProtector.Protect(plain,TimeSpan.FromSeconds(5));
            //Thread.Sleep(6000);
            //string textoDecript = limitedTimeProtector.Unprotect(cipher);
            //return Ok(new { plain, cipher, textoDecript });
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}