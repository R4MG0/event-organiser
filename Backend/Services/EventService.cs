using Backend.Persistence;
using Backend.Persistence.Entities;
using Backend.Services.Helpers;
using Backend.Services.Helpers.Auth;
using Backend.Settings;

namespace Backend.Services;

public class EventService
{
    private readonly AppDbContext _db;

    private readonly JwtService _jwtService;

    private readonly ResponseSettings _responseMessages;

    private readonly SecurityService _security;

    public EventService(AppDbContext db, JwtService jwtService, ResponseSettings responseMessages, SecurityService security)
    {
        _db = db;
        _jwtService = jwtService;
        _responseMessages = responseMessages;
        _security = security;
    }

    public ServiceResponse<List<Event>> GetAllEvents()
    {
        var events = _db.Events.ToList();

        if (events.Count == 0)
            return new ServiceResponse<List<Event>>(null, false, StatusCodes.Status204NoContent,
                "Could not find any events");

        return new ServiceResponse<List<Event>>(events, true, StatusCodes.Status200OK);
    }

}