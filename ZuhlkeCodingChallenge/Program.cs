using BusinessLogic;
using BusinessLogic.BusinessServices;
using BusinessLogic.Contracts;
using BusinessLogic.Helpers;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ZuhlkeCodingChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Store");
            Console.WriteLine();

            var fileName = string.Empty;
            if (args.Length == 0)
            {
                Console.WriteLine("Please specify file for sales data source :");
                fileName = Console.ReadLine();
            }
            else
            {
                fileName = args[0];
            }


            Task.Delay(2000);
            Console.WriteLine("...");

            if (!File.Exists(fileName))
            {
                Console.WriteLine($"The file {fileName} does not exist");
            }
            else
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();

                var dbContextOptions = new DbContextOptionsBuilder<ZuhlkeSQLDBContext>()
                    .UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                    .Options;


                var serviceProvider = new ServiceCollection()
                    .AddSingleton(new ZuhlkeDBContextOptions(dbContextOptions))
                    .AddSingleton<IDataContextFactory, DataContextFactory>()
                    .AddDbContext<ZuhlkeSQLDBContext>()
                    
                    .AddTransient<IValidationService<Sales>, CustomSalesValidationService>()
                    .AddTransient<IDatasourceReader<Sales>, CSVDataReader>()
                    .AddTransient<ISalesService, SalesService>()

                    .AddTransient<IStoreController, StoreController>()
                    .BuildServiceProvider();
                

                var storeController = serviceProvider.GetService<IStoreController>();

                Console.WriteLine();
                try
                {
                    var total = storeController.ProcessSales(new SalesCSVDataSource()
                    {
                        Name = fileName,
                        HasHeaderRecord = true
                    });

                    Console.WriteLine($"Process sales completed. Inserted {total} valid records.");
                    Console.WriteLine();

                    if (storeController.Logs.Any())
                    {
                        Console.WriteLine("Business Exceptions :");
                        foreach (var error in storeController.Logs)
                            Console.WriteLine(error.Message);
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("System Exceptions :");
                    Console.WriteLine($"Error processing datasource : {e.Message}. Inner Exception : {e.InnerException.Message}. StackTrace : {e.StackTrace}");
                }
            }

            Console.ReadKey();
        }
        
    }
}
