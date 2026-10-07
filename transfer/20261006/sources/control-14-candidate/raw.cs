public static class Candidate {
public static long Run(int[] values, int threshold)
{
    long sum = 0;
    for (int i = 0; i < values.Length; i++)
        if (values[i] == threshold)
            sum += i + 1;
    return sum;
}
}