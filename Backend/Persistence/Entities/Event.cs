namespace Backend.Persistence.Entities;

public class Event
{
    public long Id { get; set; }
    public DateTime TakesPlaceOn { get; set; }
    public string TakesPlaceAt { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }


    public User CreatedBy { get; set; }
    public DateTime CreatedAt = DateTime.Now;
    public List<User> IsAuthorizedFor = new List<User>();
}