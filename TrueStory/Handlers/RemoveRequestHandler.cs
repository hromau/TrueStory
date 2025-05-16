using MediatR;

namespace TrueStory.Handlers;

public class RemoveRequest : IRequest<string?>
{
    public RemoveRequest(string id)
    {
        Id = id;
    }

    public string Id { get; }
}

public class RemoveRequestHandler : IRequestHandler<RemoveRequest, string?>
{
    private readonly ILogger<RemoveRequestHandler> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public RemoveRequestHandler(ILogger<RemoveRequestHandler> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<string?> Handle(RemoveRequest request, CancellationToken cancellationToken)
    {
        var client = _httpClientFactory.CreateClient("Default");
        var response = await client.DeleteAsync($"objects/{request.Id}", cancellationToken);
        _logger.LogInformation("Get response: {@Content} from {Url}", response.Content,
            response.RequestMessage?.RequestUri);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync(cancellationToken);
    }
}