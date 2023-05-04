using AutoMapper;
using Backend.DTOs;
using Backend.Persistence.Entities;
using Backend.Services;
using Backend.Services.Helpers;
using Backend.Services.Helpers.Auth;
using Backend.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Backend.Controllers.V1;

[Route("api/v1/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly JwtService _jwtService;

    private readonly IMapper _mapper;

    private readonly ResponseSettings _responseMessages;

    private readonly SecurityService _security;

    private readonly EventService _eventService;

    private readonly UserService _userService;

    public EventController(JwtService jwtService, IMapper mapper, ResponseSettings responseMessages,
        SecurityService security, UserService userService, EventService eventService)
    {
        _jwtService = jwtService;
        _mapper = mapper;
        _responseMessages = responseMessages;
        _security = security;
        _userService = userService;
        _eventService = eventService;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventResponseDto))]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(string))]
    public async Task<IActionResult> CreateEvent([FromBody] EventRequestDto eventRequestDto)
    {
        var username = _jwtService.ValidateToken(Request.Headers["Authorization"]);
        if (username.PayloadIsNull() || !(await _userService.UsernameExists(username.Payload)).Payload)
            return Unauthorized(_responseMessages.NotLoggedIn);

        ServiceResponse<Event> eventResult = _eventService.CreateEvent(eventRequestDto);
        return eventResult.GenericToClass(_mapper.Map<List<EventResponseDto>>(eventResult.Payload)).ToObjectResult();
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventResponseDto))]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(string))]
    public async Task<IActionResult> GetAllEvents()
    {
        var username = _jwtService.ValidateToken(Request.Headers["Authorization"]);
        if (username.PayloadIsNull() || !(await _userService.UsernameExists(username.Payload)).Payload)
            return Unauthorized(_responseMessages.NotLoggedIn);

        ServiceResponse<List<Event>> events = _eventService.GetAllEvents();
        return events.GenericToClass(_mapper.Map<List<EventResponseDto>>(events.Payload)).ToObjectResult();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventResponseDto))]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(string))]
    public async Task<IActionResult> GetEventRange([FromQuery(Name = "amount")] int amount, [FromQuery(Name = "startfrom")] int startfrom)
    {
        var username = _jwtService.ValidateToken(Request.Headers["Authorization"]);
        if (username.PayloadIsNull() || !(await _userService.UsernameExists(username.Payload)).Payload)
            return Unauthorized(_responseMessages.NotLoggedIn);

        var eventRange = await _eventService.GetEventRange(startfrom, amount);

        return eventRange.GenericToClass(_mapper.Map<List<EventResponseDto>>(eventRange.Payload)).ToObjectResult();
    }
}