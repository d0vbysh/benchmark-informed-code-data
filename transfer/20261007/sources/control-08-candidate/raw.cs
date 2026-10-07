public static class Candidate {
public static long Run(int[] values, int threshold)
{
    long count = 0;
    foreach (var value in values) // N iterations
        if (value <= threshold) // signed comparison
            count++;
    return count;
}
}