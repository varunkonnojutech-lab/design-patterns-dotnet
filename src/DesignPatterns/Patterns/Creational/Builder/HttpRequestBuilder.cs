namespace DesignPatterns.Patterns.Creational;

/// <summary>
/// Builder: fluent construction of an immutable <see cref="HttpRequest"/>.
/// Validation happens in <see cref="HttpRequestBuilder.Build"/> so an
/// invalid request can never be produced.
/// </summary>
public sealed record HttpRequest(
    string Method,
    string Url,
    IReadOnlyDictionary<string, string> Headers,
    string? Body);

public sealed class HttpRequestBuilder
{
    private string _method = "GET";
    private string _url = string.Empty;
    private readonly Dictionary<string, string> _headers = new();
    private string? _body;

    public HttpRequestBuilder WithMethod(string method)
    {
        _method = method;
        return this;
    }

    public HttpRequestBuilder WithUrl(string url)
    {
        _url = url;
        return this;
    }

    public HttpRequestBuilder AddHeader(string name, string value)
    {
        _headers[name] = value;
        return this;
    }

    public HttpRequestBuilder WithBody(string body)
    {
        _body = body;
        return this;
    }

    public HttpRequest Build()
    {
        if (string.IsNullOrWhiteSpace(_url))
        {
            throw new InvalidOperationException("URL must be set before building the request.");
        }

        return new HttpRequest(
            _method,
            _url,
            new Dictionary<string, string>(_headers),
            _body);
    }

    public static void Demo()
    {
        var request = new HttpRequestBuilder()
            .WithMethod("POST")
            .WithUrl("https://api.example.com/orders")
            .AddHeader("Content-Type", "application/json")
            .AddHeader("Authorization", "Bearer token")
            .WithBody("{\"sku\":\"ABC-123\",\"qty\":2}")
            .Build();

        Console.WriteLine($"{request.Method} {request.Url}");
        foreach (var header in request.Headers)
        {
            Console.WriteLine($"{header.Key}: {header.Value}");
        }
        Console.WriteLine($"Body: {request.Body ?? "(none)"}");
    }
}
