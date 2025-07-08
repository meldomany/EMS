namespace EMS.Shared.DTOs
{
    public class LogHistoryDto
    {
        public int Id { get; set; }
        public string Table { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
