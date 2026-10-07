public static class Candidate {
public static long Run(int[] values, int threshold)
{
    long sum = 0;
    foreach (int value in values) // one per element
        if (value > threshold) // one per selected element
            sum += (long)value * value;
    return sum;
}
}