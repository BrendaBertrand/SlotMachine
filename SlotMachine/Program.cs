namespace SlotMachine;

class Program
{
    private const int MIN_VALUE_SLOT = 1;
    private const int MAX_VALUE_SLOT = 3;
    private const int SLOT_SIZE = 3;
    private const int LINE_COST = 1;
    private const int DECIMAL_DIGITS = 2;
    private const int JACKPOT = 7;

    private const char CENTRAL_LINE = '1';
    private const char HORIZONTAL_LINES = '2';
    private const char VERTICAL_LINES = '3';
    private const char DIAGONAL_LINES = '4';
    private const char ADD_MONEY = '5';
    private const char QUIT_GAME = '6';


    // Creation of the array
    static int[,] ArrayGenerator(Random randomGen)
    {
        int[,] slotArray = new int[SLOT_SIZE, SLOT_SIZE];
        for (int i = 0; i < SLOT_SIZE; i++)
        {
            for (int j = 0; j < SLOT_SIZE; j++)
            {
                slotArray[i, j] = randomGen.Next(MIN_VALUE_SLOT, MAX_VALUE_SLOT + 1);
            }
        }

        return slotArray;
    }

    //Check line
    static bool CheckLine(int[,] array, int line)
    {
        int equal = 0;
        for (int i = 0; i < SLOT_SIZE - 1; i++)
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

        return equal == SLOT_SIZE - 1;
    }

    //Check column
    static bool CheckColumn(int[,] array, int col)
    {
        int equal = 0;
        for (int i = 0; i < SLOT_SIZE - 1; i++)
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

        return equal == SLOT_SIZE - 1;
    }

    //Check diagonal
    static bool CheckMainDiagonal(int[,] array)
    {
        int equal = 0;
        for (int i = 0; i < SLOT_SIZE - 1; i++)
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

        return equal == SLOT_SIZE - 1;
    }

    //Check counter diagonal
    static bool CheckCounterDiagonal(int[,] array)
    {
        int equal = 0;
        for (int i = 0; i < SLOT_SIZE - 1; i++)
        {
            if (array[i, (SLOT_SIZE - 1) - i] == array[i + 1, (SLOT_SIZE - 1) - (i + 1)])
            {
                equal++;
            }
            else
            {
                break;
            }
        }

        return equal == SLOT_SIZE - 1;
    }

    //Display the array
    static void DisplayArray(int[,] array)
    {
        for (int i = 0; i < SLOT_SIZE; i++)
        {
            for (int j = 0; j < SLOT_SIZE; j++)
            {
                Console.Write($"{array[i, j]} ");
                if (j == SLOT_SIZE - 1)
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
            for (int i = 0; i < SLOT_SIZE; i++)
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
            { CENTRAL_LINE, LINE_COST },
            { HORIZONTAL_LINES, LINE_COST * SLOT_SIZE },
            { VERTICAL_LINES, LINE_COST * SLOT_SIZE },
            { DIAGONAL_LINES, LINE_COST * 2 },
            { ADD_MONEY, 0 },
        };

        Console.WriteLine("Welcome to the Slot Machine!");
        double creditAccount = GetPositiveDouble("\nPlease enter the amount of credit you want to play with : ");
        creditAccount = Double.Round(creditAccount, DECIMAL_DIGITS);
        // char[] menuOptions = new char[]{CENTRAL_LINE, HORIZONTAL_LINES, VERTICAL_LINES,DIAGONAL_LINES,ADD_MONEY};

        Console.Clear();
        do
        {
            //Menu

            Console.WriteLine("Menu : ");
            Console.WriteLine($"Press {CENTRAL_LINE} - To bet on the central line - Cost : {LINE_COST} ");
            Console.WriteLine(
                $"Press {HORIZONTAL_LINES} - To bet on all the horizontal lines - Cost : {SLOT_SIZE * LINE_COST} ");
            Console.WriteLine(
                $"Press {VERTICAL_LINES} - To bet on all the vertical lines - Cost : {SLOT_SIZE * LINE_COST} ");
            Console.WriteLine($"Press {DIAGONAL_LINES} - To bet on the two diagonals - Cost : {2 * LINE_COST} ");
            Console.WriteLine($"Press {ADD_MONEY} - To add credit - Current credit : {creditAccount} ");
            Console.WriteLine($"Press {QUIT_GAME} - To quit the game\n");

            char userChoice = Console.ReadKey().KeyChar;

            if (userChoice == QUIT_GAME)
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


            int[,] slotArray = ArrayGenerator(rng);
         
            // Check for Jackpot
            if (CheckJackpot(slotArray))
            {
                creditAccount += LINE_COST * JACKPOT;
                Console.Clear();
                DisplayArray(slotArray);
                Console.WriteLine("\nYou Won the Jackpot!\n");
                continue; 
            }

            bool isWon = false;
            switch (userChoice)
            {
                case CENTRAL_LINE:
                    if (CheckLine(slotArray, SLOT_SIZE / 2))
                    {
                        creditAccount += LINE_COST;
                        isWon = true;
                    }

                    break;
                case HORIZONTAL_LINES:
                    for (int i = 0; i < SLOT_SIZE; i++)
                    {
                        if (CheckLine(slotArray, i))
                        {
                            creditAccount += LINE_COST;
                            isWon = true;
                        }
                    }

                    break;
                case VERTICAL_LINES:
                    for (int i = 0; i < SLOT_SIZE; i++)
                    {
                        if (CheckColumn(slotArray, i))
                        {
                            creditAccount += LINE_COST;
                            isWon = true;
                        }
                    }

                    break;
                case DIAGONAL_LINES:
                    if (CheckMainDiagonal(slotArray))
                    {
                        creditAccount += LINE_COST;
                        isWon = true;
                    }

                    if (CheckCounterDiagonal(slotArray))
                    {
                        creditAccount += LINE_COST;
                        isWon = true;
                    }

                    break;
                case ADD_MONEY:
                    creditAccount +=
                        Double.Round(GetPositiveDouble("\nHow much credit do you want to add to your account ?\n "),
                            DECIMAL_DIGITS);
                    continue;
                    break;
            }


            Console.Clear();
            DisplayArray(slotArray);
            Console.WriteLine(isWon ? "You Won!\n" : "Try again\n");
        } while (true);
    }
}