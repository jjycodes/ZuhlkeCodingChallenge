using BusinessLogic.Contracts;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Helpers
{
    public abstract class BaseSalesValidationService: IValidationService<Sales>
    {
        public BaseSalesValidationService()
        {

        }
        
        //Based off from constraints set in the schema
        public virtual void Validate(IEnumerable<Sales> sales)
        {
            //Validate each record
            foreach(var sale in sales)
            {
                if (!(string.IsNullOrEmpty(sale.OrderId)
                || string.IsNullOrEmpty(sale.ProductId)
                || string.IsNullOrEmpty(sale.CustomerID)
                ))
                    sale.IsValid = true;
            }

            CheckUnique(sales.Where(x => x.IsValid).ToList());
        }
        
        public virtual void CheckUnique(IEnumerable<Sales> sales)
        {
            foreach (var sale in sales) sale.IsValid = false;

            // A better validation based on a constraint that is a combination of columns.
            // The provided schema is set to have multiple constraints on separate single columns,
            // and doesnt allow this. See derived class CustomSalesValidationService

            var firstRecordOfEveryGroup = sales.GroupBy(x => new { x.OrderId, x.CustomerID, x.ProductId})
                .Select(x => x.OrderBy(r => r.LineNumber).ThenBy(r => r.RowID).First());
            
            foreach (var sale in firstRecordOfEveryGroup) sale.IsValid = true;
            //the rest that are duplicates are not valid
        }
    }
}
