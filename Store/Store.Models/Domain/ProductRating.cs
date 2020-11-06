namespace Store.Models.Domain
{
    public class ProductRating
    {
        public ProductRating(int productId, int rating)
        {
            ProductId = productId;
            Rating = rating;
        }

        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public Product Product { get; private set; }
        public int Rating { get; private set; }
    }
}
