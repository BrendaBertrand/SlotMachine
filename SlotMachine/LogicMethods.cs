namespace SlotMachine;

public class LogicMethods
{
    // Creation of the array
    public static int[,] ArrayGenerator(Random randomGen)
    {
        int[,] slotArray = new int[Constants.SLOT_SIZE, Constants.SLOT_SIZE];
        for (int i = 0; i < Constants.SLOT_SIZE; i++)
        {
            for (int j = 0; j < Constants.SLOT_SIZE; j++)
            {
                slotArray[i, j] = randomGen.Next(Constants.MIN_VALUE_SLOT, Constants.MAX_VALUE_SLOT + 1);
            }
        }

        return slotArray;
    }
}