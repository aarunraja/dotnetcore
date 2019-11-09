
namespace MLC.General.Services
{
    using MLC.Foundation;
    using System;
    using System.Collections.Generic;
    using System.Text;


    public class BaseService
    {
        protected readonly ApplicationContext _appContext;
        public BaseService(ApplicationContext appContext)
        {
            _appContext = appContext;
        }
    }
}
