namespace ProjectMap.WebApi.Models
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public string? Reason { get; set; }
    }
}
