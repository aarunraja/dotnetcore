using System;
using System.Collections.Generic;
using System.Text;

namespace MLC.Core
{
    public class ApplicationContextProvider
    {
        public ApplicationContext Current { get; private set; }
        public ApplicationContextProvider(ApplicationContext context)
        {
            Current = context;
        }
    }
}
