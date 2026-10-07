public static class Candidate {
public static long Run(int[] values, int threshold)
{
    long sum = 0;
    foreach (int value in values)
        if (value <= threshold)
            sum += value;
    return sum;
}
}