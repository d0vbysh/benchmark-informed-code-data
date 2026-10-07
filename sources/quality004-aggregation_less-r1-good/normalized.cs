public static class Candidate {
public static long Run(int[] values, int threshold)
{
    long sum = 0;
    foreach (var value in values)
        if ((uint)value < (uint)threshold)
            sum += value;
    return sum;
}
}