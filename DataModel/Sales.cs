using System;

namespace Data
{
    public class Sales : BaseStoreEntity
    {
        public string OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShipDate { get; set; }
        public string ShipMode { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal Profit { get; set; }
        public string ProductId { get; set; }
        public string CustomerName { get; set; }
        public string Category { get; set; }
        public string CustomerID { get; set; }
        public string ProductName { get; set; }
        public int RowID { get; set; }
    }
}