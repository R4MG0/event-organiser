using AutoMapper;
using Backend.DTOs;
using Backend.DTOs.UserDto;
using Backend.Persistence.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services.Helpers;

public class ServiceResponse<T>
{
    public ServiceResponse(T payload, bool success, int statusCode, string response = "")
    {
        Payload = payload;
        Success = success;
        Response = response;
        StatusCode = statusCode;
    }

    public T Payload { get; set; }
    public bool Success { get; set; }
    public string Response { get; set; }
    public int StatusCode { get; set; }

    public bool PayloadIsNull()
    {
        return Payload is null;
    }

    public ObjectResult ToObjectResult()
    {
        return new ObjectResult(Payload)
        {
            StatusCode = StatusCode
        };
    }

    public ServiceResponse<UserResponseDto> ToUserResponseDto(string? jwt = "")
    {
        if (Payload is not User userPayload)
            throw new InvalidCastException("cannot cast " + typeof(T) + " to " + typeof(UserResponseDto));
        
        User user = (User)(object)Payload;

        UserResponseDto userResponseDto = new()
        {
            Id = user.Id,
            Jwt = jwt,
            Username = user.Username,
            CreatedAt = user.CreatedAt
        };
        return new ServiceResponse<UserResponseDto>(userResponseDto, true, StatusCodes.Status200OK);
    }
    
    public async Task<ServiceResponse<EventDto>> ToEventDto()
    {
        if (Payload is not Event eventPayload)
            throw new InvalidCastException("cannot cast " + typeof(T) + " to " + typeof(EventDto));
        
        Event event_ = (Event)(object)Payload;

        // TODO: Casting
        
        EventDto eventDto = event_ as EventDto;
        
        return new ServiceResponse<EventDto>(eventDto, true, StatusCodes.Status200OK);
    }
}