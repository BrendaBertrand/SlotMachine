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
        do
        {
            char userChoice = UIMethods.GetMenuChoice(creditAccount);
            bool isGameOn = UIMethods.CheckEndOfGame(userChoice, creditAccount);

            if (!isGameOn)
            {
                break;
            }

            bool isCorrectChoice = UIMethods.CheckUserChoice(userChoice, creditAccount);

            if (!isCorrectChoice)
            {
                continue;
            }

            creditAccount -= Constants.gameDefinition[userChoice];

            int[,] slotArray = LogicMethods.ArrayGenerator();

            // Check for Jackpot
            if (LogicMethods.CheckJackpot(slotArray))
            {
                creditAccount = LogicMethods.HandleJackpot(creditAccount, slotArray);
                continue;
            }

            // Add of credit
            if (userChoice == Constants.ADD_MONEY)
            {
                creditAccount = LogicMethods.AddCredit(creditAccount,
                    "\nHow much credit do you want to add to your account ?\n");
                continue;
            }

            // Check for gain
            double gain = LogicMethods.CheckForGain(userChoice, slotArray);
            creditAccount += gain;

            UIMethods.DisplayArray(slotArray);
            UIMethods.DisplayMessage(gain > 0 ? $"You Won {gain} credit!\n" : "Try again\n");
        } while (true);
    }
}