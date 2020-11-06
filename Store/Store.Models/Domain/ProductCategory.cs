namespace Store.Models.Domain
{
    public class ProductCategory
    {
        public int ProductId { get; private set; }
        public Product Product { get; set; }
        public int CategoryId { get; private set; }
        public Category Category { get; set; }

    }
}
