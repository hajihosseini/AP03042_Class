namespace corp2;


public abstract class CashDispenser
{
    protected CashDispenser _nextCashDispenser;

    public abstract int BankNoteUnit { get; }

    public void SetNext(CashDispenser cd)
    {
        _nextCashDispenser = cd;
    }

    public void Dispense(int amount)
    {
        int amount2dispense = amount / BankNoteUnit;
        if (amount2dispense > 0)
            System.Console.WriteLine($"{BankNoteUnit}Dispenser: Here you are: {amount2dispense} * ${BankNoteUnit}");

        amount2dispense = amount % BankNoteUnit;
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

public class CashDispenser100 : CashDispenser
{
    public override int BankNoteUnit => 100;
}

public class CashDispenser50 : CashDispenser
{
    public override int BankNoteUnit => 50;
}

public class CashDispenser20 : CashDispenser
{
    public override int BankNoteUnit => 20;
}

public class CashDispenser10 : CashDispenser
{
    public override int BankNoteUnit => 10;
}


public class CashDispenser5 : CashDispenser
{
    public override int BankNoteUnit => 5;
}

public class MyP
{
    public static void Main(string[] args)
    {
        var cd100 = new CashDispenser100();
        var cd50 = new CashDispenser50();
        var cd20 = new CashDispenser20();
        var cd10 = new CashDispenser10();
        var cd5 = new CashDispenser5();

        cd100.SetNext(cd50);
        cd50.SetNext(cd20);
        cd20.SetNext(cd10);
        cd10.SetNext(cd5);

        cd100.Dispense(5235);
        System.Console.WriteLine();
        cd100.Dispense(520);
        System.Console.WriteLine();
        cd100.Dispense(20);
        System.Console.WriteLine();
        cd100.Dispense(17);

    }
}