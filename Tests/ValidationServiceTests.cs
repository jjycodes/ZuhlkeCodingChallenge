using BusinessLogic.Helpers;
using Data;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{

    [TestFixture]
    public class ValidationServiceTests
    {
        private BaseSalesValidationService _validator;

        [OneTimeSetUp]
        public void Setup()
        {
            _validator = new CustomSalesValidationService();
        }

        [Test]
        public void AssertValidateValidData()
        {
            var sale = new Sales
            {
                OrderId = Guid.NewGuid().ToString(),
                ProductId = Guid.NewGuid().ToString(),
                CustomerID = Guid.NewGuid().ToString()
            };

            var list = new List<Sales> { sale };
            _validator.Validate(list);
            Assert.That(sale.IsValid, Is.True);
        }

        [Test]
        public void AssertValidateInvalidData()
        {
            var sale = new Sales
            {
                OrderId = Guid.NewGuid().ToString(),
                ProductId = null,
                CustomerID = Guid.NewGuid().ToString()
            };

            var list = new List<Sales> { sale };
            _validator.Validate(list);
            Assert.That(sale.IsValid, Is.False);
        }

        [Test]
        public void AssertCheckUniqueValidData()
        {
            var sale = new Sales
            {
                OrderId = Guid.NewGuid().ToString(),
                ProductId = Guid.NewGuid().ToString(),
                CustomerID = Guid.NewGuid().ToString()
            };

            var sale2 = new Sales
            {
                OrderId = Guid.NewGuid().ToString(),
                ProductId = Guid.NewGuid().ToString(),
                CustomerID = Guid.NewGuid().ToString()
            };

            var list = new List<Sales> { sale, sale2 };

            _validator.CheckUnique(list);
            Assert.That(sale.IsValid, Is.True);
            Assert.That(sale2.IsValid, Is.True);
        }

        [Test]
        public void AssertCheckUniqueInvalidData()
        {
            var sale = new Sales
            {
                OrderId = "1",
                ProductId = Guid.NewGuid().ToString(),
                CustomerID = Guid.NewGuid().ToString()
            };

            var sale2 = new Sales
            {
                OrderId = "1",
                ProductId = Guid.NewGuid().ToString(),
                CustomerID = Guid.NewGuid().ToString()
            };

            var list = new List<Sales> { sale, sale2 };

            _validator.CheckUnique(list);
            Assert.That(sale.IsValid, Is.True);
            Assert.That(sale2.IsValid, Is.False);
        }

        //Still expected to be invalid data, due to CustomSalesValidationService implementation of schema requirements
        [Test]
        public void AssertCheckUniqueInvalidData2()
        {
            var sale = new Sales
            {
                OrderId = "1",
                ProductId = Guid.NewGuid().ToString(),
                CustomerID = "2"
            };

            var sale2 = new Sales
            {
                OrderId = "1",
                ProductId = Guid.NewGuid().ToString(),
                CustomerID = Guid.NewGuid().ToString()
            };

            var sale3 = new Sales
            {
                OrderId = Guid.NewGuid().ToString(),
                ProductId = Guid.NewGuid().ToString(),
                CustomerID = "2"
            };

            var list = new List<Sales> { sale, sale2, sale3 };

            _validator.CheckUnique(list);
            Assert.That(sale.IsValid, Is.True);
            Assert.That(sale2.IsValid, Is.False);
            Assert.That(sale3.IsValid, Is.False);
        }
    }
}
