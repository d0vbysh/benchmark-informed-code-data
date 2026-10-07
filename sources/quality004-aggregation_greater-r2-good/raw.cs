public static class Candidate {
public static long Run(int[] values, int threshold)
{
    long sum = 0;
    foreach (int v in values)
        if ((uint)(v - threshold) > 0U) // signed comparison, unsigned cast
            sum += v;
    return sum;
}
}