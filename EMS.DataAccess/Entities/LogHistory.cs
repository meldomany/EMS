using System.ComponentModel.DataAnnotations;

namespace EMS.DataAccess.Entities
{
    public class LogHistory
    {
        [Key]
        public int Id { get; set; }
        public string Table { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
