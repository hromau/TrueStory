using MediatR;
using TrueStory.Models;

namespace TrueStory.Handlers;

public class GetListRequest : IRequest<List<ItemModel>>
{
    public GetListRequest(string? search, int page, int count)
    {
        Search = search;
        Page = page;
        Count = count;
    }

    public string? Search { get; }
    public int Page { get; }
    public int Count { get; }
}

public class GetListRequestHandler : IRequestHandler<GetListRequest, List<ItemModel>>
{
    private readonly ILogger<GetListRequestHandler> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public GetListRequestHandler(ILogger<GetListRequestHandler> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<ItemModel>> Handle(GetListRequest request, CancellationToken cancellationToken)
    {
        var client = _httpClientFactory.CreateClient("Default");
        var response = await client.GetAsync("objects", cancellationToken);
        _logger.LogInformation("Get response: {@Content} from {Url}", response.Content,
            response.RequestMessage?.RequestUri);
        response.EnsureSuccessStatusCode();

        var contentResponse =
            await response.Content.ReadFromJsonAsync<List<ItemModel>>(cancellationToken: cancellationToken);
        if (contentResponse is null)
        {
            _logger.LogError("Get response returned null");
            return [];
        }

        IEnumerable<ItemModel> pagedResult = contentResponse;

        if (!string.IsNullOrEmpty(request.Search))
        {
            pagedResult = pagedResult.Where(x => x.Name.ToLower().Contains(request.Search.ToLower()));
        }

        if (request.Page > 1)
        {
            pagedResult = pagedResult.Skip(request.Count * request.Page);
        }

        pagedResult = pagedResult.Take(request.Count);

        return pagedResult.ToList();
    }
}