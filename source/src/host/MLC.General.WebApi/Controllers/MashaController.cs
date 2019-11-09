namespace MLC.General.WebApi
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using MLC.Core;
    using System.Net;

    /// <summary>
    /// Masha Controller
    /// </summary>
    //[Authorize]
    public abstract class MashaController : ControllerBase
    {
        /// <summary>
        /// _logger
        /// </summary>
        protected readonly ILogger _logger;

        /// <summary>
        /// Masha Controller
        /// </summary>
        /// <param name="logger"></param>
        protected MashaController(ILogger<MashaController> logger)
        {
            _logger = logger;
        }


       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        protected ObjectResult ErrorResponse(Error e)
        {
            var statusCode = ErrorManager.FormatStatusCode(e.Code);
            switch (statusCode)
            {
                case HttpStatusCode.InternalServerError:
                    _logger.LogError(e.Exception.Message);
                    return StatusCode((int)statusCode, "Internal Error");
                default:
                    return StatusCode((int)statusCode, new { Code= e.Code,Message= e.Message });
            }
        }
        
    }
}