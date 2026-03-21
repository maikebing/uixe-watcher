namespace Uixe.Copilot.Contracts.Responses;

public enum ApiCode
{
    Error = 0,
    OK = 200,
    NotFound = 404,
    BadRequest = 500
}

public class ApiResult
{
    public ApiResult()
    {
    }

    public ApiResult(ApiCode code, string? message)
    {
        this.code = (int)code;
        msg = message;
    }

    public string? msg { get; set; }

    public int code { get; set; }
}

public class ApiResult<T>
{
    public string? msg { get; set; }

    public int code { get; set; }

    public T? data { get; set; }
}
