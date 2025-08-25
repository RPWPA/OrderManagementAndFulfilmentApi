using Domain.Enums;

namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public List<Product> Products { get; set; } = new();

        // ✅ Add Status (Pending, Paid, Cancelled, etc.)
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        // ✅ Navigation
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
