using System.Runtime.ExceptionServices;
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
        if (Payload == null)
            return new ObjectResult(Response)
            {
                StatusCode = StatusCode
            };

        return new ObjectResult(Payload)
        {
            StatusCode = StatusCode
        };
    }

    [Obsolete("ToUserResponseDto is deprecated, please use the GenericToClass method in addition to the Mapper service.")]
    public ServiceResponse<UserResponseDto> ToUserResponseDto(string? jwt = "")
    {
        // TODO: userPayload is null, why?

        if (Payload is not User userPayload)
            throw new InvalidCastException("cannot cast " + typeof(T) + " to " + typeof(UserResponseDto));

        UserResponseDto userResponseDto = new()
        {
            Id = userPayload.Id,
            Jwt = jwt,
            Username = userPayload.Username,
            CreatedAt = userPayload.CreatedAt
        };
        return new ServiceResponse<UserResponseDto>(userResponseDto, true, StatusCodes.Status200OK);
    }
    

    public ServiceResponse<T2> GenericToClass<T2>(T2 payload)
    {
        return new ServiceResponse<T2>(payload, Success, StatusCode, Response);
    }
}