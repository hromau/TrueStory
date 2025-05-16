using MediatR;
using TrueStory.Models;

namespace TrueStory.Handlers;

public class AddItemRequest : IRequest<string?>
{
    public AddItemRequest(ItemModel item)
    {
        Item = item;
    }

    public ItemModel Item { get; }
}

public class AddItemRequestHandler : IRequestHandler<AddItemRequest, string?>
{
    private readonly ILogger<AddItemRequestHandler> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public AddItemRequestHandler(ILogger<AddItemRequestHandler> logger, HttpClient client,
        IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<string?> Handle(AddItemRequest request, CancellationToken cancellationToken)
    {
        var client = _httpClientFactory.CreateClient("Default");
        var response = await client.PostAsJsonAsync("objects", request.Item, cancellationToken: cancellationToken);
        _logger.LogInformation("Get response: {@Content} from {Url}", response.Content,
            response.RequestMessage?.RequestUri);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync(cancellationToken);
    }
}
