using Store.Models.Audit;
using Store.Models.Enums;
using System;
using System.Collections.Generic;

namespace Store.Models.Domain
{
    public class Category
    {
        public Category(string name, string color)
        {
            Name = name;
            Color = color;
            Status = StatusEnum.ACTIVE;
            Log = new List<ActionLog>();
        }

        // PROPERTIES
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Color { get; private set; }
        public List<ProductCategory> Products { get; private set; }
        public StatusEnum Status { get; private set; }
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
