using BusinessLogic.Contracts;
using Data;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.BusinessServices
{
    public class SalesService : BaseService, ISalesService
    {
        private readonly IValidationService<Sales> _validationService;
        private readonly IDatasourceReader<Sales> _datasourceReader;


        public SalesService(IDataContextFactory factory,
            //ZuhlkeSQLDBContext dbContext,
            IValidationService<Sales> validationService,
            IDatasourceReader<Sales> datasourceReader) :
            base(factory)
        {
            //_dbContext = dbContext;
            _validationService = validationService;
            _datasourceReader = datasourceReader;
        }

        public IEnumerable<Sales> ReadSalesData(IDataSource dataSource, bool enforceBusinessRules)
        {
            //Parse
            var sales = _datasourceReader.Read(dataSource);

            if (enforceBusinessRules) //Validate Data
            {
                _validationService.Validate(sales);

                _errors = new List<Log>();
                foreach(var sale in sales.Where(x => !x.IsValid))
                {
                    _errors.Add(new Log {
                        LineNumber = sale.LineNumber,
                        Message = $"Sales : Validation Error found on record on Line {sale.LineNumber}/ Row {sale.RowID} with Order {sale.OrderId} from Customer {sale.CustomerID} : {sale.CustomerName}."
                    });
                }
            }

            return sales;
        }

        public void WriteSalesData(IEnumerable<Sales> sales)
        {
            using (var dbContext = _dataContextFactory.Create())
            {
                dbContext.Sales.AddRange(sales);
                dbContext.SaveChanges();
            }
        }
        
    }
}
