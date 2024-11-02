using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;
using System.Text;

namespace TP_MatiasVolpe.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ICustomAuthenticationService _customAuthenticationService;

        public AuthenticationController(IConfiguration config, ICustomAuthenticationService customAuthenticationService)
        {
            _config = config;
            _customAuthenticationService = customAuthenticationService;
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthenticationRequest authenticationRequest)
        {
            string token = _customAuthenticationService.Autenticar(authenticationRequest);
            return Ok(token);
        }
    }
}