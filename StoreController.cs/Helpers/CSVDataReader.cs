using BusinessLogic.Contracts;
using CsvHelper;
using Data;
using System;
using System.Collections.Generic;
using System.IO;

namespace BusinessLogic.Helpers
{
    public class CSVDataReader : IDatasourceReader<Sales>
    {
        public IEnumerable<Sales> Read(IDataSource dataSource)
        {
            try
            {
                var salesList = new List<Sales>();
                using (var reader = new StreamReader($"{dataSource.Name}"))
                {
                    using (var csv = new CsvReader(reader))
                    {
                        csv.Configuration.HasHeaderRecord = dataSource.HasHeaderRecord; //use order of fields if no headers, otherwise use a map
                        
                        //if (dataSource.HasHeaderRecord)
                            csv.Configuration.RegisterClassMap<CSVSalesMap>();
                        //else
                            

                        csv.Configuration.BadDataFound = context =>
                        {
                            var badDataThatCanBeActedUpon = context.RawRecord;
                        };

                        while (csv.Read() && !csv.Context.IsFieldBad)
                        {
                            salesList.Add(csv.GetRecord<Sales>());
                        }

                        return salesList;
                    }
                }
            }
            catch (ReaderException e)
            {
                //custom error handling/logging here
                throw new Exception($"Error reading data from source {dataSource.Name} on record {e.ReadingContext.RawRecord}. {e.Message} - {e.StackTrace}");
            }
            catch (Exception e)
            {
                throw new Exception($"Error reading data from source {dataSource.Name}. {e.Message} - {e.StackTrace}");
            }
        }
    }
}
