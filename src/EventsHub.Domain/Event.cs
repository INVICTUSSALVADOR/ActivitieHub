namespace EventsHub.Domain;

public class Event
{
    public string id { get; set; } = Guid.NewGuid().ToString();
    public required string Title { get; set; }
    public DateTime Date { get; set; }
    public required string Description { get; set; }
    public required string Category { get; set; }
    public bool IsCancelled { get; set; }
    public required string City { get; set; }
    public required string Venue { get; set; }
    public required string longitude { get; set; }
    public required string latitude { get; set; }

}
