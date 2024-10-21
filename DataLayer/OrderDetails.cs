using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer
{
    public class OrderDetails
    {
        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }

        public Order Order { get; set; } 
        public Product Product { get; set; }
    }
}