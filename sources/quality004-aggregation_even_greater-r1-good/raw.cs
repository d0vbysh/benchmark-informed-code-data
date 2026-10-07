public static class Candidate {
public static long Run(int[] values, int threshold)
{
    long sum = 0;
    foreach (var v in values)
        if (v > threshold && v % 2 == 0)
            sum += v;
    return sum;
}
}