public static class Candidate {
public static long Run(int[] values, int threshold)
{
    long count = 0;
    foreach (var value in values)
        if (value <= threshold)
            count++;
    return count;
}
}