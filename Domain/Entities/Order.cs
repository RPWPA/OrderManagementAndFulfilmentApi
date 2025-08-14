namespace Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public List<Product> Products { get; set; } = new();
    }
}
