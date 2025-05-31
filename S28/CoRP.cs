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

        if (_nextCashDispenser != null)
            _nextCashDispenser.Dispense(amount2dispense % BankNoteUnit);
    }
}

public class CashDispenser100: CashDispenser
{}