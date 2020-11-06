namespace Store.API.InputModels
{
    public class CategoryInputModel
    {
        public CategoryInputModel(string name, string color)
        {
            Name = name;
            Color = color;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Color { get; private set; }
    }
}
