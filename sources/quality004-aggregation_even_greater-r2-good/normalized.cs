public static class Candidate {
public static long Run(int[] values, int threshold)
{
    long sum = 0;
    foreach (int value in values)
        if ((uint)value > (uint)threshold && value % 2 == 0)
            sum += value;
    return sum;
}
}