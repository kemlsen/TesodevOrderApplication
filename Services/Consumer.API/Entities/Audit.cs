using Consumer.API.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Consumer.API.Entities
{
    public class Audit : BaseEntity
    {
        public EventType EventType { get; set; }
        public JsonDocument OldOrder { get; set; }
        public JsonDocument NewOrder { get; set; }
    }
    public enum EventType
    {
        Create,
        Update
    }
}
