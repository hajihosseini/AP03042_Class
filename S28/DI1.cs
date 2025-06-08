using System;

// Step 1: Define what we need (interface)
public interface INotificationService
{
    void SendMessage(string message);
}

// Step 2: Create implementations
public class EmailNotification : INotificationService
{
    public void SendMessage(string message)
    {
        Console.WriteLine($"📧 Email: {message}");
    }
}

public class SMSNotification : INotificationService
{
    public void SendMessage(string message)
    {
        Console.WriteLine($"📱 SMS: {message}");
    }
}

// Step 3: Create a class that needs the service
public class UserService
{
    private readonly INotificationService _notificationService;

    // Constructor Injection - the dependency is injected here
    public UserService(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public void RegisterUser(string username)
    {
        Console.WriteLine($"User {username} registered successfully!");
        
        // Use the injected service
        _notificationService.SendMessage($"Welcome {username}!");
    }
}

// Step 4: Usage examples
class Program343
{
    static void Main234()
    {
        Console.WriteLine("Simple Dependency Injection Example");
        Console.WriteLine("===================================");

        // Example 1: Using Email notification
        Console.WriteLine("\n--- Using Email Notification ---");
        INotificationService emailService = new EmailNotification();
        UserService userService1 = new UserService(emailService);
        userService1.RegisterUser("John");

        // Example 2: Using SMS notification (same UserService, different implementation)
        Console.WriteLine("\n--- Using SMS Notification ---");
        INotificationService smsService = new SMSNotification();
        UserService userService2 = new UserService(smsService);
        userService2.RegisterUser("Jane");

        Console.WriteLine("\n--- Key Benefits Demonstrated ---");
        Console.WriteLine("✅ UserService doesn't know which notification type it's using");
        Console.WriteLine("✅ We can easily switch between Email and SMS");
        Console.WriteLine("✅ Easy to add new notification types (Push, Slack, etc.)");
        Console.WriteLine("✅ Easy to test - just inject a mock service");
    }
}

// Bonus: Example of a mock for testing
public class MockNotification : INotificationService
{
    public string LastMessage { get; private set; }

    public void SendMessage(string message)
    {
        LastMessage = message;
        Console.WriteLine($"🧪 Mock: Captured message '{message}'");
    }
}

/*
WITHOUT Dependency Injection (BAD):
public class UserService
{
    private EmailNotification _notification = new EmailNotification(); // Hard-coded!
    
    public void RegisterUser(string username)
    {
        _notification.SendMessage($"Welcome {username}!");
        // Problem: Can't easily change to SMS or test with mocks
    }
}

WITH Dependency Injection (GOOD):
public class UserService
{
    private readonly INotificationService _notification;
    
    public UserService(INotificationService notification) // Flexible!
    {
        _notification = notification;
    }
    // Benefits: Can use any implementation, easy to test, flexible
}
*/