public static class Candidate {
public static long Run(int[] left, int[] right)
{
    long sum = 0;
    for (int i = 0; i < left.Length; i++)
        if (left[i] % 2 != 0 && !right.Contains(left[i]))
            sum += left[i];
    return sum;
}
}