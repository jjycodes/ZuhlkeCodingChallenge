using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;

namespace BusinessLogic.Helpers
{
    public class CustomSalesValidationService : BaseSalesValidationService
    {
        public CustomSalesValidationService()
        {
        }

        public override void CheckUnique(IEnumerable<Sales> sales)
        {
            //This can be an addition to the existing base implementation
            //base.CheckUnique(sales);

            //But can also be a replacement / override.

            foreach (var sale in sales)
            {
                sale.IsValid = false;
            }

            //This is a pain to implement because of the given schema constraints..
            //It assumes only one order for a product for a customer. Thus, some records are expected to be caught when writing to the database

            //For a better and more logical version of this validator, pls see base class implementation

            var firstRecordOfEveryOrder = sales.GroupBy(x => x.OrderId)
                .Select(x => x.OrderBy(r => r.LineNumber).ThenBy(r => r.RowID).First());

            var firstRecordOfEveryProduct = firstRecordOfEveryOrder.GroupBy(x => x.ProductId)
                .Select(x => x.OrderBy(r => r.LineNumber).ThenBy(r => r.RowID).First());

            var firstRecordOfEveryCustomer = firstRecordOfEveryProduct.GroupBy(x => x.ProductId)
                .Select(x => x.OrderBy(r => r.LineNumber).ThenBy(r => r.RowID).First());

            foreach (var sale in firstRecordOfEveryCustomer) sale.IsValid = true;
            //the rest that are duplicates are not valid
        }
    }
}
