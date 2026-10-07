public static class Candidate {
public static long Run(int[] values, int threshold)
{
    long sum = 0; // O(1)
    foreach (int value in values) // O(n)
        if (value < threshold)
            sum += Math.Abs((long)value);
    return sum; // O(1)
}
}