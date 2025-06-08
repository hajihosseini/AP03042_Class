using System;
using System.Collections.Generic;
namespace DI2;

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

// Step 1: Define interfaces (abstractions)
public interface IEmailService
{
    void SendEmail(string to, string subject, string body);
}

public interface IPaymentProcessor
{
    bool ProcessPayment(decimal amount, string customerEmail);
}

public interface IInventoryService
{
    bool CheckStock(string productName, int quantity);
    void UpdateStock(string productName, int quantity);
}

public interface IOrderRepository
{
    void SaveOrder(Order order);
    Order GetOrder(int orderId);
}

public interface ILogger
{
    void Log(string message);
}

// Step 2: Implement concrete services
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
        // Simulate payment processing
        bool success = amount > 0 && amount <= 1000; // Fail for amounts over $1000
        Console.WriteLine($"[PAYMENT] Result: {(success ? "Success" : "Failed")}");
        return success;
    }
}

public class InventoryService : IInventoryService
{
    private readonly Dictionary<string, int> _stock = new Dictionary<string, int>
    {
        { "Laptop", 10 },
        { "Mouse", 50 },
        { "Keyboard", 25 }
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

// Step 3: Create the main service that depends on other services
public class OrderService
{
    private readonly IEmailService _emailService;
    private readonly IPaymentProcessor _paymentProcessor;
    private readonly IInventoryService _inventoryService;
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger _logger;

    // Constructor Injection - dependencies are injected through constructor
    public OrderService(
        IEmailService emailService,
        IPaymentProcessor paymentProcessor,
        IInventoryService inventoryService,
        IOrderRepository orderRepository,
        ILogger logger)
    {
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        _paymentProcessor = paymentProcessor ?? throw new ArgumentNullException(nameof(paymentProcessor));
        _inventoryService = inventoryService ?? throw new ArgumentNullException(nameof(inventoryService));
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public bool ProcessOrder(Order order)
    {
        _logger.Log($"Starting order processing for {order.CustomerEmail}");

        try
        {
            // Check inventory for all items
            foreach (var item in order.Items)
            {
                if (!_inventoryService.CheckStock(item.ProductName, item.Quantity))
                {
                    _logger.Log($"Order failed: Insufficient stock for {item.ProductName}");
                    return false;
                }
            }

            // Process payment
            if (!_paymentProcessor.ProcessPayment(order.Total, order.CustomerEmail))
            {
                _logger.Log("Order failed: Payment processing failed");
                return false;
            }

            // Update inventory
            foreach (var item in order.Items)
            {
                _inventoryService.UpdateStock(item.ProductName, item.Quantity);
            }

            // Save order
            _orderRepository.SaveOrder(order);

            // Send confirmation email
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

// Step 4: Manual dependency injection (without DI container)
class Program44412
{
    static void Main234()
    {
        Console.WriteLine("Dependency Injection Example - E-commerce Order Processing");
        Console.WriteLine("===========================================================");

        // Create all dependencies
        IEmailService emailService = new EmailService();
        IPaymentProcessor paymentProcessor = new PaymentProcessor();
        IInventoryService inventoryService = new InventoryService();
        IOrderRepository orderRepository = new OrderRepository();
        ILogger logger = new ConsoleLogger();

        // Inject dependencies into OrderService
        var orderService = new OrderService(
            emailService,
            paymentProcessor,
            inventoryService,
            orderRepository,
            logger);

        // Create test orders
        var order1 = new Order
        {
            CustomerEmail = "customer@example.com",
            Total = 599.99m,
            Items = new List<OrderItem>
            {
                new OrderItem { ProductName = "Laptop", Price = 499.99m, Quantity = 1 },
                new OrderItem { ProductName = "Mouse", Price = 29.99m, Quantity = 1 },
                new OrderItem { ProductName = "Keyboard", Price = 69.99m, Quantity = 1 }
            }
        };

        var order2 = new Order
        {
            CustomerEmail = "bigspender@example.com",
            Total = 1500.00m, // This will fail payment processing
            Items = new List<OrderItem>
            {
                new OrderItem { ProductName = "Laptop", Price = 1500.00m, Quantity = 1 }
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

// Example of how you might create mock implementations for testing
public class MockEmailService : IEmailService
{
    public List<string> SentEmails = new List<string>();

    public void SendEmail(string to, string subject, string body)
    {
        SentEmails.Add($"To: {to}, Subject: {subject}");
        // No actual email sent - just recorded for testing
    }
}

/*
EXAMPLE UNIT TEST (using the mock):

[Test]
public void ProcessOrder_SuccessfulOrder_SendsConfirmationEmail()
{
    // Arrange
    var mockEmailService = new MockEmailService();
    var orderService = new OrderService(
        mockEmailService,
        new MockPaymentProcessor(),
        new MockInventoryService(),
        new MockOrderRepository(),
        new MockLogger());

    var order = new Order { CustomerEmail = "test@example.com", Total = 100 };

    // Act
    var result = orderService.ProcessOrder(order);

    // Assert
    Assert.IsTrue(result);
    Assert.AreEqual(1, mockEmailService.SentEmails.Count);
    Assert.IsTrue(mockEmailService.SentEmails[0].Contains("test@example.com"));
}
*/