public abstract class Dispenser
{
    protected Dispenser _nextDispenser;

    public void SetNextDispenser(Dispenser nextDispenser)
    {
        _nextDispenser = nextDispenser;
    }

    public abstract void Dispense(int amount);
}

public class Dispenser50 : Dispenser
{
    public override void Dispense(int amount)
    {
        int notes = amount / 50;
        if (notes > 0)
        {
            Console.WriteLine($"Dispensing {notes} x $50 notes.");
        }

        int remainingAmount = amount % 50;
        if (remainingAmount > 0)
        {
            _nextDispenser?.Dispense(remainingAmount);
        }
    }
}

public class Dispenser20 : Dispenser
{
    public override void Dispense(int amount)
    {
        int notes = amount / 20;
        if (notes > 0)
        {
            Console.WriteLine($"Dispensing {notes} x $20 notes.");
        }

        int remainingAmount = amount % 20;
        if (remainingAmount > 0)
        {
            _nextDispenser?.Dispense(remainingAmount);
        }
    }
}

public class Dispenser10 : Dispenser
{
    public override void Dispense(int amount)
    {
        int notes = amount / 10;
        if (notes > 0)
        {
            Console.WriteLine($"Dispensing {notes} x $10 notes.");
        }

        int remainingAmount = amount % 10;
        if (remainingAmount > 0)
        {
            Console.WriteLine($"Cannot dispense remaining amount of ${remainingAmount}.");
        }
    }
}

public class ATM
{
    public static void Main343(string[] args)
    {
        var dispenser50 = new Dispenser50();
        var dispenser20 = new Dispenser20();
        var dispenser10 = new Dispenser10();

        dispenser50.SetNextDispenser(dispenser20);
        dispenser20.SetNextDispenser(dispenser10);

        int amountToWithdraw = 190;
        Console.WriteLine($"Withdrawing ${amountToWithdraw}:");
        dispenser50.Dispense(amountToWithdraw);

        Console.WriteLine("\n------------------\n");

        int anotherAmount = 85;
        Console.WriteLine($"Withdrawing ${anotherAmount}:");
        dispenser50.Dispense(anotherAmount);
    }
}
