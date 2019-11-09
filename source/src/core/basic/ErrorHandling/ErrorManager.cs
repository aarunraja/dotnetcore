

namespace MLC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;
    public class ErrorManager
    {
        public static HttpStatusCode FormatStatusCode(int errorCode)
        {
            switch (errorCode)
            {
                case ErrorCodes.InputInvalid:
                case ErrorCodes.InputRequired:
                    return HttpStatusCode.BadRequest;
                case ErrorCodes.InputExists:
                case ErrorCodes.ResourceAlreadyModified:
                    return HttpStatusCode.Conflict;
                case ErrorCodes.ResourceNotFound:
                    return HttpStatusCode.NotFound;
                case ErrorCodes.NoContent:
                    return HttpStatusCode.NoContent;
                case ErrorCodes.InternalServerError:
                    return HttpStatusCode.InternalServerError;
                default:
                    return HttpStatusCode.BadRequest;
            }
        }

    }
}
