public static class Candidate {
public static long Run(int[] values, int threshold)
{
    long sum = 0;
    foreach (int value in values)
        if ((uint)(value - threshold) >= (uint)0) // no cast
            sum += value;
    return sum;
}
}