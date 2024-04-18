namespace SlotMachine;

class Program
{


    //Check line
    static bool CheckLine(int[,] array, int line)
    {
        int equal = 0;
        for (int i = 0; i < Constants.SLOT_SIZE - 1; i++)
        {
            if (array[line, i] == array[line, i + 1])
            {
                equal++;
            }
            else
            {
                break;
            }
        }

        return equal == Constants.SLOT_SIZE - 1;
    }

    //Check column
    static bool CheckColumn(int[,] array, int col)
    {
        int equal = 0;
        for (int i = 0; i < Constants.SLOT_SIZE - 1; i++)
        {
            if (array[i, col] == array[i + 1, col])
            {
                equal++;
            }
            else
            {
                break;
            }
        }

        return equal == Constants.SLOT_SIZE - 1;
    }

    //Check diagonal
    static bool CheckMainDiagonal(int[,] array)
    {
        int equal = 0;
        for (int i = 0; i < Constants.SLOT_SIZE - 1; i++)
        {
            if (array[i, i] == array[i + 1, i + 1])
            {
                equal++;
            }
            else
            {
                break;
            }
        }

        return equal == Constants.SLOT_SIZE - 1;
    }

    //Check counter diagonal
    static bool CheckCounterDiagonal(int[,] array)
    {
        int equal = 0;
        for (int i = 0; i < Constants.SLOT_SIZE - 1; i++)
        {
            if (array[i, (Constants.SLOT_SIZE - 1) - i] == array[i + 1, (Constants.SLOT_SIZE - 1) - (i + 1)])
            {
                equal++;
            }
            else
            {
                break;
            }
        }

        return equal == Constants.SLOT_SIZE - 1;
    }

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

    static double GetPositiveDouble(string question)
    {
        double value = 0;
        Console.Write(question);
        while (!Double.TryParse(Console.ReadLine(), out value) || value <= 0)
        {
            Console.WriteLine("Please enter a positive number");
        }

        return value;
    }

    static bool CheckJackpot(int[,] array)
    {
        bool isJackpot = true;
        if (CheckLine(array, 0))
        {
            for (int i = 0; i < Constants.SLOT_SIZE; i++)
            {
                if (!CheckColumn(array, i))
                {
                    isJackpot = false;
                    break;
                }
            }
        }
        else
        {
            isJackpot = false;
        }

        return isJackpot;
        
    }

    static void Main(string[] args)
    {
        Random rng = new Random();
        Dictionary<char, int> gameDefinition = new Dictionary<char, int>()
        {
            { Constants.CENTRAL_LINE, Constants.LINE_COST },
            { Constants.HORIZONTAL_LINES, Constants.LINE_COST * Constants.SLOT_SIZE },
            { Constants.VERTICAL_LINES, Constants.LINE_COST * Constants.SLOT_SIZE },
            { Constants.DIAGONAL_LINES, Constants.LINE_COST * 2 },
            { Constants.ADD_MONEY, 0 },
        };

        Console.WriteLine("Welcome to the Slot Machine!");
        double creditAccount = GetPositiveDouble("\nPlease enter the amount of credit you want to play with : ");
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

            if (!gameDefinition.Keys.Contains(userChoice))
            {
                Console.Clear();
                Console.WriteLine("Incorrect selection, Please try again. \n");
                continue;
            }


            if (gameDefinition[userChoice] > creditAccount)
            {
                Console.Clear();
                Console.WriteLine("Insufficient credit. Select another game or add credit to your account.\n");
                continue;
            }


            creditAccount -= gameDefinition[userChoice];


            int[,] slotArray = LogicMethods.ArrayGenerator(rng);
         
            // Check for Jackpot
            if (CheckJackpot(slotArray))
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
                    if (CheckLine(slotArray, Constants.SLOT_SIZE / 2))
                    {
                        creditAccount += Constants.LINE_COST;
                        isWon = true;
                    }

                    break;
                case Constants.HORIZONTAL_LINES:
                    for (int i = 0; i < Constants.SLOT_SIZE; i++)
                    {
                        if (CheckLine(slotArray, i))
                        {
                            creditAccount += Constants.LINE_COST;
                            isWon = true;
                        }
                    }

                    break;
                case Constants.VERTICAL_LINES:
                    for (int i = 0; i < Constants.SLOT_SIZE; i++)
                    {
                        if (CheckColumn(slotArray, i))
                        {
                            creditAccount += Constants.LINE_COST;
                            isWon = true;
                        }
                    }

                    break;
                case Constants.DIAGONAL_LINES:
                    if (CheckMainDiagonal(slotArray))
                    {
                        creditAccount += Constants.LINE_COST;
                        isWon = true;
                    }

                    if (CheckCounterDiagonal(slotArray))
                    {
                        creditAccount += Constants.LINE_COST;
                        isWon = true;
                    }

                    break;
                case Constants.ADD_MONEY:
                    creditAccount +=
                        Double.Round(GetPositiveDouble("\nHow much credit do you want to add to your account ?\n "),
                            Constants.DECIMAL_DIGITS);
                    continue;
                    break;
            }


            Console.Clear();
            DisplayArray(slotArray);
            Console.WriteLine(isWon ? "You Won!\n" : "Try again\n");
        } while (true);
    }
}