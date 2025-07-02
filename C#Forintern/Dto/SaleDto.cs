using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Forintern.Dto
{
    public class SaleDto
    {
        private string productCode;
        private string productName;
        private int quantity;
        private decimal unitPrice;
        private DateTime saleDate;

        public string ProductCode { get => productCode; set => productCode = value; }
        public string ProductName { get => productName; set => productName = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public decimal UnitPrice { get => unitPrice; set => unitPrice = value; }
        public decimal Total => Quantity * UnitPrice;
        public DateTime SaleDate { get => saleDate; set => saleDate = value; }
    }
}
