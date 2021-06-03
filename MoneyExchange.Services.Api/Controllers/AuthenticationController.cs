namespace MoneyExchange.Service.Api.Controllers
{
    using Core;
    using System;
    using Transversal.Common;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Configuration;

    ///<Summary>
    /// Authentication controller
    ///</Summary>
    [Route("api/auth")]
    public class AuthenticationController : BaseController
    {
        private readonly IConfiguration _configuration;

        ///<Summary>
        /// Constructor for Authentication
        ///</Summary>
        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        ///<Summary>
        /// Generate the jwt for bearer authentication
        ///</Summary>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GenerateJwt()
        {
            var tokenParams = new TokenParams
            {
                Username = "bcp_demo",
                Key = Environment.GetEnvironmentVariable("JWT_KEY"),
                Audience = _configuration?["JWT:Audience"],
                Issuer = _configuration?["JWT:Issuer"],
                ExpirationMinutes = _configuration?["JWT:ExpirationMinutes"]
            };

            var token = TokenGenerator.BuildToken(tokenParams);

            return Ok(new Response<object>
            {
                IsWarning = false,
                Data = token.TokenValue,
            });
        }
    }
}
