using System;

// Request class
public class ExpenseRequest
{
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public string Requestor { get; set; }
}

// Abstract handler
public abstract class ExpenseHandler
{
    protected ExpenseHandler _nextHandler;

    public abstract string Role { get; }
    protected abstract decimal APPROVAL_LIMIT { get; }

    public void SetNext(ExpenseHandler nextHandler)
    {
        _nextHandler = nextHandler;
    }

    public virtual void HandleRequest(ExpenseRequest request)
    {
        if (request.Amount <= APPROVAL_LIMIT)
        {
            Console.WriteLine($"{this.Role} approved expense: {request.Description} - ${request.Amount}");
        }
        else if (_nextHandler != null)
        {
            Console.WriteLine($"{this.Role} cannot approve ${request.Amount}. Forwarding to {_nextHandler.Role}...");
            _nextHandler.HandleRequest(request);
        }
        else
        {
            Console.WriteLine("No handler available for this request.");
        }

    }
}

// Concrete handlers
public class TeamLeadHandler : ExpenseHandler
{
    public override string Role => "Team Lead";

    protected override decimal APPROVAL_LIMIT => 1000m;
}

public class ManagerHandler : ExpenseHandler
{
    protected override decimal APPROVAL_LIMIT => 5000m;
    public override string Role => "Manager";    
}

public class DirectorHandler : ExpenseHandler
{
    public override string Role => "Director";

    protected override decimal APPROVAL_LIMIT => 25000m;
}

// public class CFOHandler : ExpenseHandler
// {
//     private const decimal APPROVAL_LIMIT = 40000m;

//     public override void HandleRequest(ExpenseRequest request)
//     {
//         if (request.Amount <= APPROVAL_LIMIT)
//         {
//             Console.WriteLine($"Director approved expense: {request.Description} - ${request.Amount}");
//         }
//         else if (_nextHandler != null)
//         {
//             Console.WriteLine($"Director cannot approve ${request.Amount}. Forwarding to CEO...");
//             _nextHandler.HandleRequest(request);
//         }
//         else
//         {
//             Console.WriteLine("Expense amount too high. Request denied.");
//         }
//     }
// }

public class CEOHandler : ExpenseHandler
{
    public override string Role => "CEO";

    protected override decimal APPROVAL_LIMIT => decimal.MaxValue;

    public override void HandleRequest(ExpenseRequest request)
    {
        Console.WriteLine($"CEO approved expense: {request.Description} - ${request.Amount}");
    }
}

// Usage example
class Program
{
    static void Ma45678in()
    {
        // Create handlers
        var teamLead = new TeamLeadHandler();
        var manager = new ManagerHandler();
        var director = new DirectorHandler();
        var ceo = new CEOHandler();
        
        // Set up chain
        teamLead.SetNext(manager);
        manager.SetNext(director);
        director.SetNext(ceo);
        
        // Test requests
        var requests = new[]
        {
            new ExpenseRequest { Description = "Office supplies", Amount = 500m, Requestor = "John" },
            new ExpenseRequest { Description = "New laptop", Amount = 2500m, Requestor = "Sarah" },
            new ExpenseRequest { Description = "Team building event", Amount = 8000m, Requestor = "Mike" },
            new ExpenseRequest { Description = "Office renovation", Amount = 50000m, Requestor = "Lisa" }
        };
        
        foreach (var request in requests)
        {
            Console.WriteLine($"\nProcessing request: {request.Description} (${request.Amount})");
            teamLead.HandleRequest(request);
        }
    }
}