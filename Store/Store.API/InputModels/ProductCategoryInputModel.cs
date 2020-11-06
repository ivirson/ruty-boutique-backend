namespace Store.API.InputModels
{
    public class ProductCategoryInputModel
    {
        public ProductCategoryInputModel(int categoryId)
        {
            CategoryId = categoryId;
        }

        public int CategoryId { get; private set; }
    }
}
