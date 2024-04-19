namespace SlotMachine;

public class LogicMethods
{
    // Creation of the array
    public static int[,] ArrayGenerator()
    {
        Random rng = new Random();
        int[,] slotArray = new int[Constants.SLOT_SIZE, Constants.SLOT_SIZE];
        for (int i = 0; i < Constants.SLOT_SIZE; i++)
        {
            for (int j = 0; j < Constants.SLOT_SIZE; j++)
            {
                slotArray[i, j] = rng.Next(Constants.MIN_VALUE_SLOT, Constants.MAX_VALUE_SLOT + 1);
            }
        }

        return slotArray;
    }

    //Check line
    public static bool CheckLine(int[,] array, int line)
    {
        for (int i = 0; i < Constants.SLOT_SIZE - 1; i++)
        {
            if (array[line, i] != array[line, i + 1])
            {
                return false;
            }
        }
        return true;

    }

    //Check column
    public static bool CheckColumn(int[,] array, int col)
    {
        
        for (int i = 0; i < Constants.SLOT_SIZE - 1; i++)
        {
            if (array[i, col] != array[i + 1, col])
            {
                return false;
            }
        }
        return true;
    }

    //Check diagonal
    public static bool CheckMainDiagonal(int[,] array)
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
    public static bool CheckCounterDiagonal(int[,] array)
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

    public static bool CheckJackpot(int[,] array)
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

    public static double HandleJackpot(double creditAccount, int[,] slotArray)
    {
        creditAccount += Constants.LINE_COST * Constants.JACKPOT;
        UIMethods.DisplayArray(slotArray);
        UIMethods.DisplayMessage("\nYou Won the Jackpot!\n");
        return creditAccount;
    }

    public static double GetPositiveDouble(string question)
    {
        double value = 0;
        UIMethods.DisplayMessage(question);
        while (!Double.TryParse(Console.ReadLine(), out value) || value <= 0)
        {
            UIMethods.DisplayMessage("Please enter a positive number\n");
        }

        return value;
    }

    public static double CheckForGain(char userChoice, int[,] slotArray)
    {
        double gain = 0;
        switch (userChoice)
        {
            case Constants.CENTRAL_LINE:
                if (CheckLine(slotArray, Constants.SLOT_SIZE / 2))
                {
                    gain = Constants.LINE_COST;
                }

                break;
            case Constants.HORIZONTAL_LINES:
                for (int i = 0; i < Constants.SLOT_SIZE; i++)
                {
                    if (CheckLine(slotArray, i))
                    {
                        gain += Constants.LINE_COST;
                    }
                }

                break;
            case Constants.VERTICAL_LINES:
                for (int i = 0; i < Constants.SLOT_SIZE; i++)
                {
                    if (CheckColumn(slotArray, i))
                    {
                        gain += Constants.LINE_COST;
                    }
                }

                break;
            case Constants.DIAGONAL_LINES:
                if (CheckMainDiagonal(slotArray))
                {
                    gain = Constants.LINE_COST;
                }

                if (CheckCounterDiagonal(slotArray))
                {
                    gain += Constants.LINE_COST;
                }

                break;
        }

        return gain;
    }

    public static double AddCredit(double creditAccount, string question)
    {
        creditAccount +=
            Double.Round(
                GetPositiveDouble(question),
                Constants.DECIMAL_DIGITS);
        UIMethods.ClearUI();
        return creditAccount;
    }
}