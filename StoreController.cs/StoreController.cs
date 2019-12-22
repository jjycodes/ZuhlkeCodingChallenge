using BusinessLogic.Contracts;
using Data;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class StoreController : IStoreController
    {
        private readonly ISalesService _salesService;

        public StoreController(ISalesService salesService)
        {
            _salesService = salesService;
        }

        public int ProcessSales(IDataSource dataSource, bool enforceBusinessRules)
        {
            var sales = _salesService.ReadSalesData(dataSource, enforceBusinessRules);

            var validSales = sales.Where(x => x.IsValid);
            _salesService.WriteSalesData(validSales);

            return validSales.Count();
        }

        public IEnumerable<Log> Logs => _salesService != null ? (_salesService as BaseService).Errors : Enumerable.Empty<Log>();
        
    }
}
