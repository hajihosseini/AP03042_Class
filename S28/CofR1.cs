using System;
using System.IO;

// Log levels
public enum LogLevel
{
    Debug = 1,
    Info = 2,
    Warning = 3,
    Error = 4,
    Fatal = 5
}

// Log message
public class LogMessage
{
    public LogLevel Level { get; set; }
    public string Message { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;
}

// Abstract logger
public abstract class Logger
{
    protected LogLevel _logLevel;
    protected Logger _nextLogger;
    
    protected Logger(LogLevel logLevel)
    {
        _logLevel = logLevel;
    }
    
    public void SetNext(Logger nextLogger)
    {
        _nextLogger = nextLogger;
    }
    
    public void Log(LogMessage message)
    {
        if (message.Level >= _logLevel)
        {
            WriteMessage(message);
        }
        
        // Always pass to next logger in chain
        _nextLogger?.Log(message);
    }
    
    protected abstract void WriteMessage(LogMessage message);
}

// Concrete loggers
public class ConsoleLogger : Logger
{
    public ConsoleLogger(LogLevel logLevel) : base(logLevel) { }
    
    protected override void WriteMessage(LogMessage message)
    {
        ConsoleColor originalColor = Console.ForegroundColor;
        
        // Set color based on log level
        Console.ForegroundColor = message.Level switch
        {
            LogLevel.Debug => ConsoleColor.Gray,
            LogLevel.Info => ConsoleColor.White,
            LogLevel.Warning => ConsoleColor.Yellow,
            LogLevel.Error => ConsoleColor.Red,
            LogLevel.Fatal => ConsoleColor.DarkRed,
            _ => ConsoleColor.White
        };
        
        Console.WriteLine($"[CONSOLE] {message.Timestamp:yyyy-MM-dd HH:mm:ss} [{message.Level}] {message.Message}");
        Console.ForegroundColor = originalColor;
    }
}

public class FileLogger : Logger
{
    private readonly string _filePath;
    
    public FileLogger(LogLevel logLevel, string filePath) : base(logLevel)
    {
        _filePath = filePath;
    }
    
    protected override void WriteMessage(LogMessage message)
    {
        try
        {
            string logEntry = $"[FILE] {message.Timestamp:yyyy-MM-dd HH:mm:ss} [{message.Level}] {message.Message}{Environment.NewLine}";
            File.AppendAllText(_filePath, logEntry);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to write to file: {ex.Message}");
        }
    }
}

public class EmailLogger : Logger
{
    private readonly string _emailAddress;
    
    public EmailLogger(LogLevel logLevel, string emailAddress) : base(logLevel)
    {
        _emailAddress = emailAddress;
    }
    
    protected override void WriteMessage(LogMessage message)
    {
        // Simulate sending email (in real implementation, you'd use an email service)
        Console.WriteLine($"[EMAIL] Sending to {_emailAddress}: [{message.Level}] {message.Message}");
    }
}

// Usage example
class LoggingExample
{
    static void Main234()
    {
        // Create loggers with different levels
        var consoleLogger = new ConsoleLogger(LogLevel.Debug);
        var fileLogger = new FileLogger(LogLevel.Warning, "app.log");
        var emailLogger = new EmailLogger(LogLevel.Error, "admin@company.com");
        
        // Set up chain: Console -> File -> Email
        consoleLogger.SetNext(fileLogger);
        fileLogger.SetNext(emailLogger);
        
        // Test different log levels
        var messages = new[]
        {
            new LogMessage { Level = LogLevel.Debug, Message = "Debug information" },
            new LogMessage { Level = LogLevel.Info, Message = "Application started" },
            new LogMessage { Level = LogLevel.Warning, Message = "Low disk space" },
            new LogMessage { Level = LogLevel.Error, Message = "Database connection failed" },
            new LogMessage { Level = LogLevel.Fatal, Message = "System crash detected" }
        };
        
        Console.WriteLine("Processing log messages through chain:");
        Console.WriteLine("=====================================");
        
        foreach (var message in messages)
        {
            Console.WriteLine($"\nLogging: {message.Level} - {message.Message}");
            consoleLogger.Log(message);
        }
    }
}