using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

// Custom HTTP message handlers that form a chain
public class LoggingHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"[HTTP_LOG] Sending {request.Method} request to {request.RequestUri}");
        
        // Call the next handler in the chain
        var response = await base.SendAsync(request, cancellationToken);
        
        Console.WriteLine($"[HTTP_LOG] Received {response.StatusCode} response");
        return response;
    }
}

public class AuthenticationHandler : DelegatingHandler
{
    private readonly string _token;
    
    public AuthenticationHandler(string token)
    {
        _token = token;
    }
    
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Add authentication header before calling next handler
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
        Console.WriteLine("[HTTP_AUTH] Added authentication header");
        
        return await base.SendAsync(request, cancellationToken);
    }
}

public class RetryHandler : DelegatingHandler
{
    private readonly int _maxRetries;
    
    public RetryHandler(int maxRetries = 3)
    {
        _maxRetries = maxRetries;
    }
    
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        HttpResponseMessage response = null;
        
        for (int attempt = 1; attempt <= _maxRetries; attempt++)
        {
            try
            {
                response = await base.SendAsync(request, cancellationToken);
                
                if (response.IsSuccessStatusCode)
                {
                    return response;
                }
                
                Console.WriteLine($"[HTTP_RETRY] Attempt {attempt} failed with status {response.StatusCode}");
            }
            catch (HttpRequestException ex) when (attempt < _maxRetries)
            {
                Console.WriteLine($"[HTTP_RETRY] Attempt {attempt} failed with exception: {ex.Message}");
            }
            
            if (attempt < _maxRetries)
            {
                await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, attempt)), cancellationToken);
            }
        }
        
        return response ?? throw new HttpRequestException("All retry attempts failed");
    }
}

// Usage example
class HttpClientChainExample
{
    static async Task Main55()
    {
        // Create the chain of handlers
        var retryHandler = new RetryHandler(3);
        var authHandler = new AuthenticationHandler("my-secret-token");
        var loggingHandler = new LoggingHandler();
        
        // Chain them together
        retryHandler.InnerHandler = authHandler;
        authHandler.InnerHandler = loggingHandler;
        loggingHandler.InnerHandler = new HttpClientHandler(); // Final handler
        
        // Create HttpClient with the handler chain
        using var httpClient = new HttpClient(retryHandler);
        
        try
        {
            Console.WriteLine("Making HTTP request through handler chain:");
            Console.WriteLine("=========================================");
            
            var response = await httpClient.GetAsync("https://httpbin.org/get");
            var content = await response.Content.ReadAsStringAsync();
            
            Console.WriteLine($"\nResponse received successfully!");
            Console.WriteLine($"Status: {response.StatusCode}");
            Console.WriteLine($"Content length: {content.Length} characters");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Request failed: {ex.Message}");
        }
    }
}