namespace MLC.General.WebApi
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;
    using MLC.Core;
    using MLC.General.Contract;
    using MLC.General.Services;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// TokenController
    /// </summary>
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/authorize")]
    [ApiController]
    public class AuthorizeController : MashaController
    {
        private readonly IConfiguration _config;
        private readonly AccountService _accountService;


        /// <summary>
        /// Authorize Controller
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="config"></param>
        /// <param name="appContext"></param>
        /// <param name="accountsRepository"></param>
        /// <param name="emailService"></param>
        public AuthorizeController(ILogger<AuthorizeController> logger, IConfiguration config, ApplicationContext appContext, Domain.IAccountRepository accountsRepository, IEmailService emailService)
            : base(logger)
        {
            _config = config;
            _accountService = new AccountService(appContext, accountsRepository, emailService);
        }

        /// <summary>
        /// CreateToken
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody]AccountLogin login)
        {
            var result = await _accountService.ValidateUser(login);
            return (result.HasValue) ?BuildToken(result.value) : ErrorResponse(result.Error);
        }

        private IActionResult BuildToken(Account user)
        {
            if (user == null)
            {
                return Unauthorized();
            }

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(CustomClaim.UserId, user.Id),
                new Claim(CustomClaim.FirstName, user.FirstName),
                new Claim(CustomClaim.LastName, user.LastName),
                new Claim(CustomClaim.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Security:Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Security:Tokens:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMonths(1),
              signingCredentials: creds);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { token = tokenString });
        }
    }
}
