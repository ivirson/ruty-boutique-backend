using Store.API.ViewModels;
using System.Collections.Generic;

namespace Store.API.InputModels
{
    public class ProductInputModel
    {
        public ProductInputModel(string name, string description, decimal price, string color, int id)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Color = color;
            Sizes = new List<ProductSizeViewModel>();
            Categories = new List<ProductCategoryInputModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public List<ProductCategoryInputModel> Categories { get; set; }
        public List<ProductSizeViewModel> Sizes { get; set; }
    }
}
