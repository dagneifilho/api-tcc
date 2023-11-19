namespace Domain.Models;

public class IncidentsFiltersDb
{
    public int? IssuerId { get; set; }
    public int? ClosedById { get; set; }
    public bool? Solved { get; set; }
    public int? SectionId { get; set; }
}