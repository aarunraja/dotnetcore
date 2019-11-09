

namespace MLC.General.Services
{
    using MLC.General.Contract;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class AggregateAdapter
    {
        public static AggregateContract ToContract(this Domain.AggregateDom aggregateDom)
        {
            return new AggregateContract()
            {
                PropertyName = aggregateDom.PropertyName,
                Count = aggregateDom.Count
            };
        }
    }
}
