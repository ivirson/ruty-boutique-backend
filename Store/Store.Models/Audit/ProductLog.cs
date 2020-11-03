using Store.Models.Domain;

namespace Store.Models.Audit
{
    public class ProductLog
    {
        public ProductLog(int productId)
        {
            ProductId = productId;
        }

        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public Product Product { get; private set; }
    }
}
