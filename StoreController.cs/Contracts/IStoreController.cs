using Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Contracts
{
    public interface IStoreController
    {
        int ProcessSales(IDataSource dataSource, bool enforceBusinessRules = true);
        IEnumerable<Log> Logs { get; }
    }
}
