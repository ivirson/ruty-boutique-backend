using System.Collections.Generic;

namespace Store.API.ViewModels
{
    public class CategoryViewModel
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
