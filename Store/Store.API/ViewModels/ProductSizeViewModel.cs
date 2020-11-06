namespace Store.API.ViewModels
{
    public class ProductSizeViewModel
    {
        public ProductSizeViewModel() { }

        public ProductSizeViewModel(string size, int qty)
        {
            Size = size;
            Qty = qty;
        }

        public string Size { get; set; }
        public int Qty { get; set; }
    }
}
