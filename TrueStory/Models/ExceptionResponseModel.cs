namespace TrueStory.Models;

public sealed record ExceptionResponseModel
{
    public string? OriginalMessage { get; set; }
    public string? OriginalStatusCode { get; set; }
    public string? OriginalUrl { get; set; }
}