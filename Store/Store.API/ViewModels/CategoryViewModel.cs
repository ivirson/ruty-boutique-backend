﻿namespace Store.API.ViewModels
{
    public class CategoryViewModel
    {
        public CategoryViewModel(int id, string name, string color)
        {
            Id = id;
            Name = name;
            Color = color;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
