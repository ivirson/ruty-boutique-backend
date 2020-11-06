using System.Collections.Generic;

namespace Store.API.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel(string name, string description, decimal price, string color, string productCode)
        {
            Name = name;
            Description = description;
            Price = price;
            Color = color;
            ProductCode = productCode;
            Categories = new List<ProductCategoryViewModel>();
            Sizes = new List<ProductSizeViewModel>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string ProductCode { get; private set; }
        public List<ProductCategoryViewModel> Categories { get; set; }
        public List<ProductSizeViewModel> Sizes { get; set; }
        public int Rating { get; set; }
        public int Qty { get; set; }
    }
}
