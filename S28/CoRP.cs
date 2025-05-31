namespace corp2;


public abstract class CashDispenser
{
    protected CashDispenser _nextCashDispenser;

    public abstract int BankNoteUnit { get; }

    public void Dispense(int amount)
    {
        int amount2dispense = amount / BankNoteUnit;
        if (amount2dispense > 0)
            System.Console.WriteLine($"{BankNoteUnit}Dispenser: Here you are: ${amount2dispense}");

        amount2dispense = amount2dispense % BankNoteUnit;
        if (_nextCashDispenser != null)
        {
            if (amount2dispense > 0)
                _nextCashDispenser.Dispense(amount2dispense);
        }
        else if (amount2dispense > 0)
        {
            System.Console.WriteLine($"ERROR: Cannot dipsense this amount: {amount}");
        }
    }
}

public class CashDispenser100: CashDispenser
{}