using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.API.Application.Events
{
    public class OrderChangedEvent
    {
        public EventType EventType { get; set; }
        public Domain.Entities.Order OldOrder { get; set; }
        public Domain.Entities.Order NewOrder { get; set; }
        public DateTime CreatedAt { get; set; }

        public OrderChangedEvent()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
    public enum EventType
    {
        Create,
        Update
    }
}
