﻿using Gateway.Application.DTO.Log;
using Gateway.Application.Interfaces;
using Gateway.Domain.Entities;

namespace Gateway.Application.Mappings;

public class LogMapper : ILogMapper
{
    public Log FromLogRequest(LogRequest LogResponse) => new Log(
        null,
        LogResponse.ClientIP,
        LogResponse.Method,
        LogResponse.Request,
        LogResponse.Response,
        LogResponse.Date,
        LogResponse.StatusCode,
        LogResponse.Message
    );

    public LogResponse ToLogResponse(Log log) => new()
    {
        Id = log.Id,
        ClientIP = log.ClientIP,
        Method = log.Method,
        StatusCode = log.StatusCode,
        Request = log.Request,
        Response = log.Response,
        Date = log.Date,
        Message = log.Message
    };
}