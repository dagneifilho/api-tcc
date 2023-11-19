namespace Domain.Models;

public class Incident
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int Issuer { get; set; }
    public int SectionId { get; set; }
    public bool Solved { get; set; }
    public int? ClosedBy { get; set; }
    public DateTime Opened { get; set; }
    public DateTime? Closed { get; set; }
}