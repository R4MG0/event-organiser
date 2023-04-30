using AutoMapper;
using Backend.DTOs;
using Backend.Services;
using Backend.Services.Helpers.Auth;
using Backend.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Backend.Controllers.V1;

[Microsoft.AspNetCore.Components.Route("api/v1/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly JwtService _jwtService;

    private readonly IMapper _mapper;

    private readonly ResponseSettings _responseMessages;

    private readonly SecurityService _security;

    private readonly EventService _eventService;

    private readonly UserService _userService;

    public EventController(JwtService jwtService, IMapper mapper, ResponseSettings responseMessages, SecurityService security, UserService userService, EventService eventService)
    {
        _jwtService = jwtService;
        _mapper = mapper;
        _responseMessages = responseMessages;
        _security = security;
        _userService = userService;
        _eventService = eventService;
    }
    
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventDto))]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(string))]
    public async Task<IActionResult> GetAllEvents([FromQuery(Name = "amount", )] int amount)
    {
        var username = _jwtService.ValidateToken(Request.Headers["Authorization"]);
        if (username.PayloadIsNull() || !(await _userService.UsernameExists(username.Payload)).Payload)
            return Unauthorized(_responseMessages.NotLoggedIn);

        if (amount <= 0)
        {
            return _eventService.GetAllEvents(). ToObjectResult();
        }


        return serviceResponse.();
    }
    
    
}