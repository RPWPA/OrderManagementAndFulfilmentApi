using Domain.Enums;

namespace Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }

        // Link to Order
        public int OrderId { get; set; }
        public Order Order { get; set; }

        // Payment Info
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "EUR";

        public string Provider { get; set; } // e.g. Stripe, PayPal
        public string TransactionId { get; set; } // gateway transaction id

        public PaymentStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
