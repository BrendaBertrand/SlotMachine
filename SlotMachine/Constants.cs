namespace SlotMachine;

public class Constants
{
    public const int MIN_VALUE_SLOT = 1;
    public const int MAX_VALUE_SLOT = 3;
    public const int SLOT_SIZE = 3;
    public const int LINE_COST = 1;
    public const int DECIMAL_DIGITS = 2;
    public const int JACKPOT = 7;

    public const char CENTRAL_LINE = '1';
    public const char HORIZONTAL_LINES = '2';
    public const char VERTICAL_LINES = '3';
    public const char DIAGONAL_LINES = '4';
    public const char ADD_MONEY = '5';
    public const char QUIT_GAME = '6';
    
    public static Dictionary<char, int> gameDefinition = new Dictionary<char, int>()
    {
        { CENTRAL_LINE, LINE_COST },
        { HORIZONTAL_LINES, LINE_COST * SLOT_SIZE },
        { VERTICAL_LINES, LINE_COST * SLOT_SIZE },
        { DIAGONAL_LINES, LINE_COST * 2 },
        { ADD_MONEY, 0 },
    };

}