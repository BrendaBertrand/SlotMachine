namespace SlotMachine;

class Program
{
    static void Main(string[] args)
    {
        UIMethods.DisplayMessage("Welcome to the Slot Machine!");
        double creditAccount = 0;

        creditAccount =
            LogicMethods.AddCredit(creditAccount, "\nPlease enter the amount of credit you want to play with : ");


        UIMethods.ClearUI();
        LogicMethods.GlobalTurn(creditAccount);
    }
}