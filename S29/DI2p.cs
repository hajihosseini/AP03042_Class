using System;
using System.Collections.Generic;
namespace DI2p;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;


class Program
{
    static void Main3(string[] args)
    {
        // The Host provides a standardized way to set up DI, logging, and configuration.
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                // Step 1: Register all your services with the DI container
                // We define the interface and the concrete class that implements it.

                // Singleton: A single instance is created and shared for the entire application lifetime.
                // Good for services that hold state, like our inventory and order data.
                services.AddSingleton<IInventoryService, InventoryService>();
                services.AddSingleton<IOrderRepository, OrderRepository>();
                services.AddSingleton<IEmailService, EmailService>();
                services.AddSingleton<IPaymentProcessor, PaymentProcessor>();
                services.AddSingleton<ILogger, ConsoleLogger>();

                // Transient: A new instance is created every time it's requested.
                // Good for the main service that performs the work.
                services.AddTransient<OrderService>();
            })
            .Build();

        // Step 2: Resolve the main service from the DI container.
        // The container automatically injects all the registered dependencies into its constructor.
        var orderService = host.Services.GetRequiredService<OrderService>();

        Console.WriteLine("Dependency Injection Example - E-commerce Order Processing");
        Console.WriteLine("===========================================================");

        // --- The rest of the logic is the same, but now it uses the DI-managed OrderService ---

        // Create test orders
        var order1 = new Order
        {
            CustomerEmail = "customer@example.com",
            Total = 599.99m,
            Items = new List<OrderItem>
                {
                    new OrderItem { ProductName = "Laptop", Quantity = 1 },
                    new OrderItem { ProductName = "Mouse", Quantity = 1 },
                    new OrderItem { ProductName = "Keyboard", Quantity = 1 }
                }
        };

        var order2 = new Order
        {
            CustomerEmail = "bigspender@example.com",
            Total = 1500.00m, // This will fail payment processing
            Items = new List<OrderItem>
                {
                    new OrderItem { ProductName = "Laptop", Quantity = 1 }
                }
        };

        // Process orders
        Console.WriteLine("\n--- Processing Order 1 ---");
        bool result1 = orderService.ProcessOrder(order1);
        Console.WriteLine($"Order 1 Result: {(result1 ? "SUCCESS" : "FAILED")}");

        Console.WriteLine("\n--- Processing Order 2 ---");
        bool result2 = orderService.ProcessOrder(order2);
        Console.WriteLine($"Order 2 Result: {(result2 ? "SUCCESS" : "FAILED")}");
    }
}

// --- All other classes (Order, OrderItem, IEmailService, EmailService, etc.) remain unchanged ---

// Models
public class Order
{
    public int Id { get; set; }
    public string CustomerEmail { get; set; }
    public decimal Total { get; set; }
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
}

public class OrderItem
{
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

// Interfaces (Abstractions)
public interface IEmailService { void SendEmail(string to, string subject, string body); }
public interface IPaymentProcessor { bool ProcessPayment(decimal amount, string customerEmail); }
public interface IInventoryService { bool CheckStock(string productName, int quantity); void UpdateStock(string productName, int quantity); }
public interface IOrderRepository { void SaveOrder(Order order); Order GetOrder(int orderId); }
public interface ILogger { void Log(string message); }

// Concrete Implementations
public class EmailService : IEmailService
{
    public void SendEmail(string to, string subject, string body)
    {
        Console.WriteLine($"[EMAIL] Sending to {to}");
        Console.WriteLine($"[EMAIL] Subject: {subject}");
        Console.WriteLine($"[EMAIL] Body: {body}");
    }
}

public class PaymentProcessor : IPaymentProcessor
{
    public bool ProcessPayment(decimal amount, string customerEmail)
    {
        Console.WriteLine($"[PAYMENT] Processing ${amount} for {customerEmail}");
        bool success = amount > 0 && amount <= 1000;
        Console.WriteLine($"[PAYMENT] Result: {(success ? "Success" : "Failed")}");
        return success;
    }
}

public class InventoryService : IInventoryService
{
    private readonly Dictionary<string, int> _stock = new Dictionary<string, int>
        {
            { "Laptop", 10 }, { "Mouse", 50 }, { "Keyboard", 25 }
        };

    public bool CheckStock(string productName, int quantity)
    {
        if (_stock.ContainsKey(productName))
        {
            bool available = _stock[productName] >= quantity;
            Console.WriteLine($"[INVENTORY] {productName}: {(available ? "Available" : "Out of stock")}");
            return available;
        }
        return false;
    }

    public void UpdateStock(string productName, int quantity)
    {
        if (_stock.ContainsKey(productName))
        {
            _stock[productName] -= quantity;
            Console.WriteLine($"[INVENTORY] Updated {productName} stock: {_stock[productName]} remaining");
        }
    }
}

public class OrderRepository : IOrderRepository
{
    private readonly Dictionary<int, Order> _orders = new Dictionary<int, Order>();
    private int _nextId = 1;

    public void SaveOrder(Order order)
    {
        order.Id = _nextId++;
        _orders[order.Id] = order;
        Console.WriteLine($"[REPOSITORY] Order {order.Id} saved to database");
    }

    public Order GetOrder(int orderId)
    {
        _orders.TryGetValue(orderId, out Order order);
        return order;
    }
}

public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"[LOG] {DateTime.Now:HH:mm:ss} - {message}");
    }
}

// The main service class remains unchanged, its dependencies are provided by DI.
public class OrderService
{
    private readonly IEmailService _emailService;
    private readonly IPaymentProcessor _paymentProcessor;
    private readonly IInventoryService _inventoryService;
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger _logger;

    public OrderService(
        IEmailService emailService,
        IPaymentProcessor paymentProcessor,
        IInventoryService inventoryService,
        IOrderRepository orderRepository,
        ILogger logger)
    {
        _emailService = emailService;
        _paymentProcessor = paymentProcessor;
        _inventoryService = inventoryService;
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public bool ProcessOrder(Order order)
    {
        _logger.Log($"Starting order processing for {order.CustomerEmail}");

        try
        {
            foreach (var item in order.Items)
            {
                if (!_inventoryService.CheckStock(item.ProductName, item.Quantity))
                {
                    _logger.Log($"Order failed: Insufficient stock for {item.ProductName}");
                    return false;
                }
            }

            if (!_paymentProcessor.ProcessPayment(order.Total, order.CustomerEmail))
            {
                _logger.Log("Order failed: Payment processing failed");
                return false;
            }

            foreach (var item in order.Items)
            {
                _inventoryService.UpdateStock(item.ProductName, item.Quantity);
            }

            _orderRepository.SaveOrder(order);

            _emailService.SendEmail(
                order.CustomerEmail,
                "Order Confirmation",
                $"Your order #{order.Id} for ${order.Total} has been processed successfully!");

            _logger.Log($"Order processing completed successfully for {order.CustomerEmail}");
            return true;
        }
        catch (Exception ex)
        {
            _logger.Log($"Order processing failed: {ex.Message}");
            return false;
        }
    }
}
