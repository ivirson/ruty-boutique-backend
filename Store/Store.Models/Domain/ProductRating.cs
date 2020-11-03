namespace Store.Models.Domain
{
    public class ProductRating
    {
        public ProductRating(int productId)
        {
            ProductId = productId;
        }

        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public Product Product { get; private set; }
    }
}
