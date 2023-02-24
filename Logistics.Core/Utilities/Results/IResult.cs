﻿namespace Logistics.Core.Utilities.Results
{
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }

        int StatusCode { get; set; }
    }
}