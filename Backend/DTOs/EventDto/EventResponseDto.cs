using Backend.Persistence.Entities;

namespace Backend.DTOs;

public class EventResponseDto
{
    public User CreatedBy;
    public DateTime CreatedAt = DateTime.Now;
    public DateTime TakesPlaceOn;
    public string TakesPlaceAt = string.Empty;
    public string Title = string.Empty;
    public string Description = string.Empty;
    public string Image = string.Empty;
    public List<User> IsAuthorizedFor = new List<User>();
}