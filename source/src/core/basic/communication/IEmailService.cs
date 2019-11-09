namespace MLC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IEmailService
    {
        Task SendEmail(string email, string subject, string message);
    }
}
