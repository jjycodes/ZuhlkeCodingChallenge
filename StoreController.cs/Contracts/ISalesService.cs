using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Contracts
{
    public interface ISalesService
    {
        IEnumerable<Sales> ReadSalesData(IDataSource dataSource, bool enforceBusinessRules = true);

        void WriteSalesData(IEnumerable<Sales> sales);
    }
}
