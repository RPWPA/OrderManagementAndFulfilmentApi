namespace Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        // Parent-Child Relationship
        public int? ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }

        public List<Category> Subcategories { get; set; } = new();

        public List<Product> Products { get; set; } = new();
    }
}
