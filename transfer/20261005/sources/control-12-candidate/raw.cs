public static class Candidate {
public static long Run(int[] values, int threshold)
{
    long sum = 0;
    foreach (var value in values)
        if (value < threshold)
            sum += Math.Abs((long)value);
    return sum;
}
}