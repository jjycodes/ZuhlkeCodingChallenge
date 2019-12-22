using CsvHelper.Configuration;
using Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BusinessLogic.Helpers
{
    
    public class CSVSalesMap : ClassMap<Sales>
    {
        private const string DATEFORMAT = "dd.MM.yy";
        public CSVSalesMap()
        {
            //properties to csv fields mapping

            Map(x => x.Quantity);
            Map(x => x.Discount);
            Map(x => x.Profit);
            Map(x => x.Category);

            Map(x => x.OrderId).Name("Order ID");
            
                //.Validate(x => !string.IsNullOrEmpty(x));  // Ideally you should be able to do validations here. but lets set this aside for now, so that the system manages to continue despite reading invalid data

            Map(x => x.OrderDate).Name("Order Date")
                .TypeConverterOption.Format(DATEFORMAT);

            Map(x => x.ShipDate).Name("Ship Date")
                .TypeConverterOption.Format(DATEFORMAT);

            Map(x => x.ShipMode).Name("Ship Mode");

            Map(x => x.CustomerID).Name("Customer ID");

            Map(x => x.CustomerName).Name("Customer Name");

            Map(x => x.ProductId).Name("Product ID");

            Map(x => x.ProductName).Name("Product Name");

            Map(m => m.RowID).Name("Row ID"); //prone to typo errors, could be missing

            Map(m => m.LineNumber).ConvertUsing(x => x.Context.RawRow); //Introduce one that is sequential, for error tracking
        }
        
    }


}
