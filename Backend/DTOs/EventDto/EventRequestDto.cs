using Backend.Persistence.Entities;

namespace Backend.DTOs;

public class EventRequestDto
{
    public DateTime TakesPlaceOn;
    public string TakesPlaceAt = string.Empty;
    public string Title = string.Empty;
    public string Description = string.Empty;
    public string Image = string.Empty;
}