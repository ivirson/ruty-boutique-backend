using Store.Models.Core;
using Store.Models.Domain;
using Store.Models.Enums;

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
        public int UserId { get; private set; }
        public User User { get; private set; }
        public LogTypeEnum Type { get; private set; }
    }
}
