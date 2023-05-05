using Microsoft.Build.Framework;

namespace Backend.DTOs.EventDto;

public class EventRequestDto
{
    [Required] public DateTime TakesPlaceOn { get; set; }
    [Required] public string TakesPlaceAt { get; set; }
    [Required] public string Title { get; set; }
    [Required] public string Description { get; set; }
    [Required] public string Image { get; set; }
}