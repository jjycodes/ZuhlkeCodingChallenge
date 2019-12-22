using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.Contracts;
using Data;
using BusinessLogic.Helpers;

namespace Tests
{
    [TestFixture]
    public class DataReaderTests
    {
        private IDatasourceReader<Sales> _datasourceReader;

        [OneTimeSetUp]
        public void Setup()
        {
            _datasourceReader = new CSVDataReader();
        }

        [Test]
        public void GivenDataSourceReader_WhenReadingValidDataSourceWithHeader_ThenReturnsList()
        {
            var dataSourceMock = Mock.Of<SalesCSVDataSource>(x => x.Name == "sales.csv" &&
                x.HasHeaderRecord == true
            );

            var list = _datasourceReader.Read(dataSourceMock);
            Assert.That(list, Is.Not.Null);
            Assert.That(list, Is.Not.Empty);
        }
        
    }
}
