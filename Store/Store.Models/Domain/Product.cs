using Store.Models.Audit;
using Store.Models.Enums;
using System;
using System.Collections.Generic;

namespace Store.Models.Domain
{
    public class Product
    {
        public Product(string name, string description, decimal price, string color)
        {
            Name = name;
            Description = description;
            Price = price;
            Color = color;
            Status = StatusEnum.ACTIVE;
            Sizes = new List<ProductSize>();
            Categories = new List<ProductCategory>();
            Ratings = new List<ProductRating>();
            Log = new List<ActionLog>();
        }

        // PROPERTIES
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public string Color { get; private set; }
        public List<ProductSize> Sizes { get; private set; }
        public StatusEnum Status { get; private set; }
        public List<ProductCategory> Categories { get; private set; }
        public List<ProductRating> Ratings { get; private set; }
        public List<ActionLog> Log { get; private set; }

        // METHODS
        public void SetInactive()
        {
            if (Status != StatusEnum.ACTIVE)
            {
                throw new Exception("Estado invalido.");
            }
            Status = StatusEnum.INACTIVE;
        }

        public void SetActive()
        {
            if (Status != StatusEnum.INACTIVE)
            {
                throw new Exception("Estado invalido.");
            }
            Status = StatusEnum.ACTIVE;
        }
    }
}
