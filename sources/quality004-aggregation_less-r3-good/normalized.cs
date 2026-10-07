public static class Candidate {
public static long Run(int[] values, int threshold)
{
    long sum = 0;
    foreach (int v in values)
        if ((uint)v < (uint)threshold)
            sum += v;
    return sum;
}
}