namespace SlotMachine;

public class UIMethods
{
    //Display the array
    public static void DisplayArray(int[,] array)
    {
        Console.Clear();
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

    // Display the Menu 

    public static void DisplayMenu(double creditAccount)
    {
        DisplayMessage("Menu : ");
        DisplayMessage(
            $"Press {Constants.CENTRAL_LINE} - To bet on the central line - Cost : {Constants.LINE_COST} ");
        DisplayMessage(
            $"Press {Constants.HORIZONTAL_LINES} - To bet on all the horizontal lines - Cost : {Constants.SLOT_SIZE * Constants.LINE_COST} ");
        DisplayMessage(
            $"Press {Constants.VERTICAL_LINES} - To bet on all the vertical lines - Cost : {Constants.SLOT_SIZE * Constants.LINE_COST} ");
        DisplayMessage(
            $"Press {Constants.DIAGONAL_LINES} - To bet on the two diagonals - Cost : {2 * Constants.LINE_COST} ");
        DisplayMessage($"Press {Constants.ADD_MONEY} - To add credit - Current credit : {creditAccount} ");
        DisplayMessage($"Press {Constants.QUIT_GAME} - To quit the game\n");
    }

    public static char GetMenuChoice(double creditAccount)
    {
        DisplayMenu(creditAccount);
        char userChoice = Console.ReadKey().KeyChar;
        return userChoice;
    }

    public static bool CheckEndOfGame(char userChoice, double creditAccount)
    {
        bool isGameOn = true;
        if (userChoice == Constants.QUIT_GAME)
        {
            DisplayMessage($"\nGame is over. You have {creditAccount} credits.\n");
            isGameOn = false;
        }
        return isGameOn;
    }

    public static bool CheckUserChoice(char userChoice, double creditAccount)
    {
        bool isCorrectChoice = true;
        if (!Constants.gameDefinition.Keys.Contains(userChoice))
        {
            Console.Clear();
            DisplayMessage("Incorrect selection, Please try again. \n");
            isCorrectChoice = false;
        }
        else if (Constants.gameDefinition[userChoice] > creditAccount)
        {
            Console.Clear();
            DisplayMessage("Insufficient credit. Select another game or add credit to your account.\n");
            isCorrectChoice = false;
        }

        return isCorrectChoice;

    }

    public static void DisplayMessage(string message)
    {
        Console.WriteLine(message);
    }
}