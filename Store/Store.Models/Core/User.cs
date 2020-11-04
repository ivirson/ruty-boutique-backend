using Store.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Models.Core
{
    public class User
    {
        public User(string name, string surname, string email, string password)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Password = password;
            Status = StatusEnum.ACTIVE;
        }

        // PROPERTIES
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public UserProfileEnum Profile { get; private set; }
        public StatusEnum Status { get; private set; }

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
