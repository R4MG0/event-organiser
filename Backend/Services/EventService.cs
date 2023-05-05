using AutoMapper;
using Backend.DTOs;
using Backend.DTOs.EventDto;
using Backend.Persistence;
using Backend.Persistence.Entities;
using Backend.Services.Helpers;
using Backend.Services.Helpers.Auth;
using Backend.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;

namespace Backend.Services;

public class EventService
{
    private readonly AppDbContext _db;

    private readonly JwtService _jwtService;
    
    private readonly IMapper _mapper;

    private readonly ResponseSettings _responseMessages;

    private readonly SecurityService _security;

    public EventService(AppDbContext db, JwtService jwtService, IMapper mapper, IOptions<ResponseSettings> responseMessages,
        SecurityService security)
    {
        _db = db;
        _jwtService = jwtService;
        _mapper = mapper;
        _responseMessages = responseMessages.Value;
        _security = security;
    }

    public async Task<ServiceResponse<Event>> CreateEvent(EventRequestDto eventRequestDto, User createdBy)
    {
        Event event_ = _mapper.Map<Event>(eventRequestDto);

        event_.CreatedBy = createdBy;
        event_.IsAuthorizedFor = new List<User>
        {
            createdBy
        };

        EntityEntry<Event> returnevent = await _db.Events.AddAsync(event_);
        await _db.SaveChangesAsync();

        return new ServiceResponse<Event>(returnevent.Entity, true, StatusCodes.Status201Created);
    }

    public ServiceResponse<List<Event>> GetAllEvents()
    {
        var events = _db.Events.ToList();

        if (events.Count == 0)
            return new ServiceResponse<List<Event>>(null, false, StatusCodes.Status204NoContent,
                "Could not find any events");

        return new ServiceResponse<List<Event>>(events, true, StatusCodes.Status200OK);
    }

    public async Task<ServiceResponse<List<Event>>> GetEventRange(int startfrom, int amount)
    {
        if (startfrom < 0)
            return new ServiceResponse<List<Event>>(null, false, StatusCodes.Status204NoContent,
                "Could not find any events");

        var events = (await _db.Events.OrderBy(x => x.Id).ToListAsync()).GetRange(startfrom, amount);

        return new ServiceResponse<List<Event>>(events, true, StatusCodes.Status200OK);
    }
}