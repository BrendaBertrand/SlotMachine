namespace SlotMachine;

class Program
{




    

    //Display the array
    static void DisplayArray(int[,] array)
    {
        for (int i = 0; i < Constants.SLOT_SIZE; i++)
        {
            for (int j = 0; j < Constants.SLOT_SIZE; j++)
            {
                Console.Write($"{array[i, j]} ");
                if (j == Constants.SLOT_SIZE - 1)
                {
                    Console.Write("\n");
                }
            }
        }
    }





    static void Main(string[] args)
    {
        Random rng = new Random();


        Console.WriteLine("Welcome to the Slot Machine!");
        double creditAccount = LogicMethods.GetPositiveDouble("\nPlease enter the amount of credit you want to play with : ");
        creditAccount = Double.Round(creditAccount, Constants.DECIMAL_DIGITS);
        // char[] menuOptions = new char[]{CENTRAL_LINE, HORIZONTAL_LINES, VERTICAL_LINES,DIAGONAL_LINES,ADD_MONEY};

        Console.Clear();
        do
        {
            //Menu

            Console.WriteLine("Menu : ");
            Console.WriteLine($"Press {Constants.CENTRAL_LINE} - To bet on the central line - Cost : {Constants.LINE_COST} ");
            Console.WriteLine(
                $"Press {Constants.HORIZONTAL_LINES} - To bet on all the horizontal lines - Cost : {Constants.SLOT_SIZE * Constants.LINE_COST} ");
            Console.WriteLine(
                $"Press {Constants.VERTICAL_LINES} - To bet on all the vertical lines - Cost : {Constants.SLOT_SIZE * Constants.LINE_COST} ");
            Console.WriteLine($"Press {Constants.DIAGONAL_LINES} - To bet on the two diagonals - Cost : {2 * Constants.LINE_COST} ");
            Console.WriteLine($"Press {Constants.ADD_MONEY} - To add credit - Current credit : {creditAccount} ");
            Console.WriteLine($"Press {Constants.QUIT_GAME} - To quit the game\n");

            char userChoice = Console.ReadKey().KeyChar;

            if (userChoice == Constants.QUIT_GAME)
            {
                Console.WriteLine($"\nGame is over. You have {creditAccount} credits.");
                break;
            }

            if (!Constants.gameDefinition.Keys.Contains(userChoice))
            {
                Console.Clear();
                Console.WriteLine("Incorrect selection, Please try again. \n");
                continue;
            }


            if (Constants.gameDefinition[userChoice] > creditAccount)
            {
                Console.Clear();
                Console.WriteLine("Insufficient credit. Select another game or add credit to your account.\n");
                continue;
            }


            creditAccount -= Constants.gameDefinition[userChoice];


            int[,] slotArray = LogicMethods.ArrayGenerator(rng);
         
            // Check for Jackpot
            if (LogicMethods.CheckJackpot(slotArray))
            {
                creditAccount += Constants.LINE_COST * Constants.JACKPOT;
                Console.Clear();
                DisplayArray(slotArray);
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
                        Double.Round(LogicMethods.GetPositiveDouble("\nHow much credit do you want to add to your account ?\n "),
                            Constants.DECIMAL_DIGITS);
                    continue;
                    
            }


            Console.Clear();
            DisplayArray(slotArray);
            Console.WriteLine(isWon ? "You Won!\n" : "Try again\n");
        } while (true);
    }
}