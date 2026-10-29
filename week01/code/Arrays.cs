public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // PLAN FOR MultiplesOf:
        // 1. Create a new fixed array of doubles with size = length. This will hold our results.
        // 2. Loop through the array using an index i from 0 to length-1.
        // 3. For each position, the multiple is number * (i+1). 
        //    Example: if number=7, i=0 => 7*1=7, i=1 => 7*2=14, etc.
        // 4. Store that value into the array at index i.
        // 5. After the loop finishes, return the completed array.

        double[] result = new double[length];
        for (int i = 0; i < length; i++)
        {
            result[i] = number * (i + 1);
        }
        return result;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // PLAN FOR RotateListRight:
        // 1. Understand rotation: rotating right by 'amount' means the last 'amount' items
        //    move to the front of the list.
        // 2. Handle edge case: if amount == data.Count, the list ends up the same, so we can return early
        //    (or let the logic handle it - it will still work).
        // 3. Use List slicing with GetRange to avoid manual index wrapping:
        //    a. Find the split point: splitIndex = data.Count - amount
        //    b. Get tail = last 'amount' items -> data.GetRange(splitIndex, amount)
        //    c. Get head = first 'splitIndex' items -> data.GetRange(0, splitIndex)
        // 4. Clear the original list and rebuild it in rotated order:
        //    a. data.Clear()
        //    b. data.AddRange(tail) first
        //    c. data.AddRange(head) second
        // 5. Alternative logic using modulo would be: create new list, for i from 0 to Count-1,
        //    newIndex = (i + amount) % Count, but GetRange method is simpler and meets hint.
        // This modifies the existing list in place as required, not returning a new one.

        if (data.Count == 0 || amount == 0)
            return;

        // Normalize amount in case amount == Count
        int splitIndex = data.Count - amount;
        
        // If amount equals Count, splitIndex is 0, no change needed
        if (splitIndex == 0)
            return;

        List<int> tail = data.GetRange(splitIndex, amount);
        List<int> head = data.GetRange(0, splitIndex);

        data.Clear();
        data.AddRange(tail);
        data.AddRange(head);
    }
}