using BusinessLogic.BusinessServices;
using BusinessLogic.Contracts;
using Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class SalesServiceTests
    {
        private ISalesService _salesService;

        [OneTimeSetUp]
        public void Setup()
        {
            var validationServiceMock = new Mock<IValidationService<Sales>>();

            var datasourceReaderMock = new Mock<IDatasourceReader<Sales>>();
            datasourceReaderMock.Setup(m => m.Read(It.IsAny<SalesCSVDataSource>())).Returns(() => new List<Sales>());

            var dbContextMock = new Mock<IDatabaseContext>();
            dbContextMock.Setup(m => m.SaveChanges())
                .Returns(() => 1);

            var dbSetMock = new Mock<DbSet<Sales>>();

            dbContextMock.Setup(m => m.Sales)
                .Returns(() => dbSetMock.Object);


            var dataContextFactoryMock = new Mock<IDataContextFactory>();
            dataContextFactoryMock.Setup(m => m.Create())
                .Returns(dbContextMock.Object);

            
            _salesService = new SalesService(dataContextFactoryMock.Object,
                validationServiceMock.Object, datasourceReaderMock.Object);
        }

        [Test]
        public void GivenSalesService_WhenReadingSalesData_ThenReturnsSalesEnumerable()
        {
            var datasource = new Mock<IDataSource>();
            var data = _salesService.ReadSalesData(datasource.Object, false);
            Assert.That(data, Is.Not.Null);
            Assert.That(data, Is.Empty);
        }

        [Test]
        public void GivenSalesService_EnforcedBusinessRules_WhenReadingSalesData_ThenReturnsSalesEnumerable()
        {
            var datasource = new Mock<IDataSource>();
            var data = _salesService.ReadSalesData(datasource.Object, true);
            Assert.That(data, Is.Not.Null);
            Assert.That(data, Is.Empty);
        }

        [Test]
        public void GivenSalesService_WhenWritingSalesData_ThenCompletes()
        {
            _salesService.WriteSalesData(new List<Sales>());
        }
    }
}