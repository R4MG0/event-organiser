using Backend.DTOs.UserDto;
using Backend.Persistence.Entities;

namespace Backend.DTOs.EventDto;

public class EventResponseDto
{
    public long Id { get; set; }
    public UserResponseDto CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime TakesPlaceOn { get; set; }
    public string TakesPlaceAt { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public List<UserResponseDto> IsAuthorizedFor = new List<UserResponseDto>();
}