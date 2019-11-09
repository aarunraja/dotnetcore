namespace MLC.General.WebApi
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MLC.General.Contract;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MLC.Core;
    using System;
    using System.Net;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Http;
    using MLC.General.Services;

    /// <summary>
    /// Accounts
    /// </summary>
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/Accounts")]

    [ApiController]
    public class AccountsController : MashaController
    {
        private readonly AccountService _AccountService;


        /// <summary>
        /// Accounts Controllers
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="appContext"></param>
        /// <param name="accountsRepository"></param>
        /// <param name="emailService"></param>
        public AccountsController(ILogger<AccountsController> logger, ApplicationContext appContext, Domain.IAccountRepository accountsRepository, IEmailService emailService)
            : base(logger)
        {
            _AccountService = new AccountService(appContext, accountsRepository, emailService);
        }

        /// <summary>
        /// Get all Accounts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> Get(string q = "")
        {
            var result = await _AccountService.GetAllUsers(q);
            return (result.HasValue) ? ErrorResponse(result.Error) : Ok(result.value);
        }

        /// <summary>
        /// Get Account by EmailId
        /// </summary>
        /// <param name="id">Id is an Email</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetById(string id)
        {
            var result = await _AccountService.GetUser(id);
            return (result.HasError) ? ErrorResponse(result.Error) : Ok(result.value);
        }

        /// <summary>
        /// Create Account
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Account>> Post([FromBody] AccountUpsert account)
        {
            var result = await _AccountService.Create(account);
            return (result.HasError) ? ErrorResponse(result.Error) : CreatedAtAction(nameof(GetById), new { id = result.value.Id }, result.value);
        }

        /// <summary>
        /// Update Account
        /// </summary>
        /// <param name="id">id is unique id</param>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Put(string id, [FromBody] AccountUpsert account)
        {
            var result = await _AccountService.Update(id, account);
            return (result.HasValue) ? ErrorResponse(result.Error) : Ok(result.value);
        }

        /// <summary>
        /// Change Password 
        /// </summary>
        /// <param name="changePassword"></param>
        /// <returns></returns>
        [HttpPut("ChangePassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePassword changePassword)
        {
            var result = await _AccountService.ChangePassword(changePassword);
            return (result.HasValue) ? ErrorResponse(result.Error) : Ok(result.value);
        }

        /// <summary>
        /// Reset Password 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPut("PasswordReset")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> PasswordReset(string email)
        {
            var result = await _AccountService.ResetPassword(email);
            return (result.HasValue) ? ErrorResponse(result.Error) : Ok(result.value);
        }

        /// <summary>
        /// Delete Account
        /// </summary>
        /// <param name="id">Id is unique id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await _AccountService.Delete(id);
            return (result.HasValue) ? ErrorResponse(result.Error) : Ok(result.value);
        }
    }
}
