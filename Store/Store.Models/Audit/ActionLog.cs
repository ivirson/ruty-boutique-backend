using Store.Models.Core;
using Store.Models.Domain;
using Store.Models.Enums;
using System;

namespace Store.Models.Audit
{
    public class ActionLog
    {
        public ActionLog(int entityId, int userId, LogTypeEnum type, EntitiesEnum entity)
        {
            EntityId = entityId;
            UserId = userId;
            Type = type;
            Entity = entity;
            Date = DateTime.Now;
        }

        public int Id { get; private set; }
        public int EntityId { get; private set; }
        public Product Product { get; private set; }
        public int UserId { get; private set; }
        public User User { get; private set; }
        public EntitiesEnum Entity { get; private set; }
        public LogTypeEnum Type { get; private set; }
        public DateTime Date { get; private set; }
    }
}
