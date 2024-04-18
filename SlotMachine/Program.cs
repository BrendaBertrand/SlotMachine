namespace SlotMachine;

class Program
{
    static void Main(string[] args)
    {
        Random rng = new Random();
        
        Console.WriteLine("Welcome to the Slot Machine!");
        double creditAccount =
            LogicMethods.GetPositiveDouble("\nPlease enter the amount of credit you want to play with : ");
        creditAccount = Double.Round(creditAccount, Constants.DECIMAL_DIGITS);
        bool isGameOn;
        bool isCorrectChoice;
        char userChoice;
        Console.Clear();
        do
        {
            //Menu
            userChoice = UIMethods.GetMenuChoice(creditAccount);
            isGameOn = UIMethods.CheckEndOfGame(userChoice, creditAccount);
            if (!isGameOn)
            {
                break;
            }

            isCorrectChoice = UIMethods.CheckUserChoice(userChoice, creditAccount);

            if (!isCorrectChoice)
            {
                continue;
            }


            creditAccount -= Constants.gameDefinition[userChoice];


            int[,] slotArray = LogicMethods.ArrayGenerator(rng);

            // Check for Jackpot
            if (LogicMethods.CheckJackpot(slotArray))
            {
                creditAccount += Constants.LINE_COST * Constants.JACKPOT;
                Console.Clear();
                UIMethods.DisplayArray(slotArray);
                Console.WriteLine("\nYou Won the Jackpot!\n");
                continue;
            }

            bool isWon = false;
            switch (userChoice)
            {
                case Constants.CENTRAL_LINE:
                    if (LogicMethods.CheckLine(slotArray, Constants.SLOT_SIZE / 2))
                    {
                        creditAccount += Constants.LINE_COST;
                        isWon = true;
                    }

                    break;
                case Constants.HORIZONTAL_LINES:
                    for (int i = 0; i < Constants.SLOT_SIZE; i++)
                    {
                        if (LogicMethods.CheckLine(slotArray, i))
                        {
                            creditAccount += Constants.LINE_COST;
                            isWon = true;
                        }
                    }

                    break;
                case Constants.VERTICAL_LINES:
                    for (int i = 0; i < Constants.SLOT_SIZE; i++)
                    {
                        if (LogicMethods.CheckColumn(slotArray, i))
                        {
                            creditAccount += Constants.LINE_COST;
                            isWon = true;
                        }
                    }

                    break;
                case Constants.DIAGONAL_LINES:
                    if (LogicMethods.CheckMainDiagonal(slotArray))
                    {
                        creditAccount += Constants.LINE_COST;
                        isWon = true;
                    }

                    if (LogicMethods.CheckCounterDiagonal(slotArray))
                    {
                        creditAccount += Constants.LINE_COST;
                        isWon = true;
                    }

                    break;
                case Constants.ADD_MONEY:
                    creditAccount +=
                        Double.Round(
                            LogicMethods.GetPositiveDouble("\nHow much credit do you want to add to your account ?\n "),
                            Constants.DECIMAL_DIGITS);
                    continue;
            }


            Console.Clear();
            UIMethods.DisplayArray(slotArray);
            Console.WriteLine(isWon ? "You Won!\n" : "Try again\n");
        } while (true);
    }
}