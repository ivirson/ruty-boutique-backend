using System.ComponentModel.DataAnnotations;

namespace Store.Models.Domain
{
    public class ProductSize
    {
        public ProductSize() { }
        public ProductSize(int productId, string size, int qty)
        {
            ProductId = productId;
            Size = size;
            Qty = qty;
        }

        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public Product Product { get; private set; }
        [StringLength(3)]
        public string Size { get; private set; }
        public int Qty { get; private set; }

        public void UpdateQty(int qty)
        {
            Qty = qty;
        }
    }
}
