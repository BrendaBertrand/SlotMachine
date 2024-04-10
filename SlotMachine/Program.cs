namespace SlotMachine;

class Program
{
    private const int MIN_VALUE_SLOT = 1;
    private const int MAX_VALUE_SLOT = 3;
    private const int SLOT_SIZE = 3;
    private const char DIAGONAL_DIRECTION = 'd';
    private const char LINE_DIRECTION = 'l';
    private const char COLUMN_DIRECTION = 'c';

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

    static void Main(string[] args)
    {
        Random rng = new Random();
   
        int[,] slotArray = ArrayGenerator(rng);

        DisplayArray(slotArray);

        Console.WriteLine($"The first line is a {(CheckLine(slotArray, 0) ? "win" : "loose")}");
        Console.WriteLine($"The second line is a {(CheckLine(slotArray, 1)? "win" : "loose")}");
        Console.WriteLine($"The third line is a {(CheckLine(slotArray, 2)? "win" : "loose")}\n");
        Console.WriteLine($"The first column is a {(CheckColumn(slotArray, 0)? "win" : "loose")}");
        Console.WriteLine($"The second column is a {(CheckColumn(slotArray, 1)? "win" : "loose")}");
        Console.WriteLine($"The third column is a {(CheckColumn(slotArray, 2)? "win" : "loose")}\n");
        Console.WriteLine($"The main diagonal is a {(CheckMainDiagonal(slotArray)? "win" : "loose")}");
        Console.WriteLine($"The counter diagonal is a {(CheckCounterDiagonal(slotArray)? "win" : "loose")}");
 
    }
}