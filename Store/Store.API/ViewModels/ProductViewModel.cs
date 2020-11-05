using System.Collections.Generic;

namespace Store.API.ViewModels
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public List<ProductSizeViewModel> Sizes { get; set; }
        public int Rating { get; set; }
        public int Qty { get; set; }
    }
}
