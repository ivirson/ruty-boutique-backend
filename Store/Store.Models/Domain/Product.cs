using Store.Models.Audit;
using Store.Models.Enums;
using System;
using System.Collections.Generic;

namespace Store.Models.Domain
{
    public class Product
    {
        public Product(string name, string description, decimal price, int qty, string color)
        {
            Name = name;
            Description = description;
            Price = price;
            Qty = qty;
            Color = color;
            Status = StatusEnum.ACTIVE;
            Categories = new List<Category>();
            Ratings = new List<ProductRating>();
            Log = new List<ProductLog>();
        }

        // PROPERTIES
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Qty { get; set; }
        public string Color { get; private set; }
        public StatusEnum Status { get; private set; }
        public List<Category> Categories { get; private set; }
        public List<ProductRating> Ratings { get; private set; }
        public List<ProductLog> Log { get; private set; }

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

        public void UpdateQty(int qty)
        {
            Qty = qty;
        }
    }
}
