using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace DI2;


// Using the same interfaces and models from the previous example
// (IEmailService, IPaymentProcessor, etc.)

// Configuration class to demonstrate different service lifetimes
public class DatabaseConfig
{
    public string ConnectionString { get; set; } = "Server=localhost;Database=Orders;";
}

// Enhanced services using .NET's ILogger
public class EnhancedEmailService : IEmailService
{
    private readonly ILogger<EnhancedEmailService> _logger;

    public EnhancedEmailService(ILogger<EnhancedEmailService> logger)
    {
        _logger = logger;
    }

    public void SendEmail(string to, string subject, string body)
    {
        _logger.LogInformation("Sending email to {Email} with subject: {Subject}", to, subject);
        // Simulate email sending
        Console.WriteLine($"[EMAIL SERVICE] Email sent to {to}");
    }
}

public class EnhancedOrderRepository : IOrderRepository
{
    private readonly DatabaseConfig _config;
    private readonly ILogger<EnhancedOrderRepository> _logger;
    private readonly Dictionary<int, Order> _orders = new Dictionary<int, Order>();
    private int _nextId = 1;

    public EnhancedOrderRepository(DatabaseConfig config, ILogger<EnhancedOrderRepository> logger)
    {
        _config = config;
        _logger = logger;
    }

    public void SaveOrder(Order order)
    {
        order.Id = _nextId++;
        _orders[order.Id] = order;
        _logger.LogInformation("Saved order {OrderId} to database using connection: {ConnectionString}", 
            order.Id, _config.ConnectionString);
    }

    public Order GetOrder(int orderId)
    {
        _orders.TryGetValue(orderId, out Order order);
        _logger.LogInformation("Retrieved order {OrderId} from database", orderId);
        return order;
    }
}

// Background service that processes orders
public class OrderProcessingService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<OrderProcessingService> _logger;

    public OrderProcessingService(IServiceScopeFactory scopeFactory, ILogger<OrderProcessingService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Order Processing Service started");

        // Simulate processing orders periodically
        while (!stoppingToken.IsCancellationRequested)
        {
            // Create a new scope for each operation (important for scoped services)
            using var scope = _scopeFactory.CreateScope();
            var orderService = scope.ServiceProvider.GetRequiredService<OrderService>();

            // Process a sample order
            var order = new Order
            {
                CustomerEmail = "automated@example.com",
                Total = 99.99m,
                Items = new List<OrderItem>
                {
                    new OrderItem { ProductName = "Mouse", Price = 99.99m, Quantity = 1 }
                }
            };

            _logger.LogInformation("Processing automated order");
            var result = orderService.ProcessOrder(order);
            _logger.LogInformation("Automated order result: {Result}", result ? "Success" : "Failed");

            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken); // Process every 30 seconds
        }
    }
}

// Main program with DI container setup
class Program
{
    static async Task Main(string[] args)
    {
        // Create host builder with DI container
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                // Register configuration
                services.AddSingleton(new DatabaseConfig
                {
                    ConnectionString = "Server=localhost;Database=OrdersDB;Trusted_Connection=true;"
                });

                // Register services with different lifetimes
                services.AddSingleton<IInventoryService, InventoryService>(); // Singleton - one instance for entire app
                services.AddScoped<IOrderRepository, EnhancedOrderRepository>(); // Scoped - one instance per request/scope
                services.AddTransient<IEmailService, EnhancedEmailService>(); // Transient - new instance every time
                services.AddTransient<IPaymentProcessor, PaymentProcessor>();

                // Register the main service
                services.AddScoped<OrderService>();

                // Register background service
                services.AddHostedService<OrderProcessingService>();
            })
            .Build();

        // Demonstrate manual service resolution
        Console.WriteLine(".NET Dependency Injection Container Example");
        Console.WriteLine("===========================================");

        using (var scope = host.Services.CreateScope())
        {
            // Get services from the container
            var orderService = scope.ServiceProvider.GetRequiredService<OrderService>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            logger.LogInformation("Starting manual order processing demonstration");

            // Create and process test orders
            var orders = new[]
            {
                new Order
                {
                    CustomerEmail = "customer1@example.com",
                    Total = 199.98m,
                    Items = new List<OrderItem>
                    {
                        new OrderItem { ProductName = "Keyboard", Price = 69.99m, Quantity = 1 },
                        new OrderItem { ProductName = "Mouse", Price = 29.99m, Quantity = 1 }
                    }
                },
                new Order
                {
                    CustomerEmail = "customer2@example.com",
                    Total = 1200.00m, // This will fail
                    Items = new List<OrderItem>
                    {
                        new OrderItem { ProductName = "Laptop", Price = 1200.00m, Quantity = 1 }
                    }
                }
            };

            foreach (var order in orders)
            {
                logger.LogInformation("Processing order for {CustomerEmail}", order.CustomerEmail);
                var result = orderService.ProcessOrder(order);
                logger.LogInformation("Order result: {Result}", result ? "Success" : "Failed");
                Console.WriteLine();
            }
        }

        // Start the background service
        Console.WriteLine("Starting background order processing service...");
        Console.WriteLine("Press Ctrl+C to stop");

        await host.RunAsync();
    }
}

/*
Key DI Container Concepts Demonstrated:

1. SERVICE LIFETIMES:
   - Singleton: One instance for entire application lifetime
   - Scoped: One instance per scope/request
   - Transient: New instance every time it's requested

2. SERVICE REGISTRATION:
   - AddSingleton<TInterface, TImplementation>()
   - AddScoped<TInterface, TImplementation>()
   - AddTransient<TInterface, TImplementation>()

3. SERVICE RESOLUTION:
   - GetRequiredService<T>() - throws if not found
   - GetService<T>() - returns null if not found

4. SCOPING:
   - CreateScope() for manual scope management
   - IServiceScopeFactory for creating scopes in background services

5. INTEGRATION:
   - Works seamlessly with .NET's logging framework
   - Integrates with hosted services and background workers
   - Supports configuration injection
*/