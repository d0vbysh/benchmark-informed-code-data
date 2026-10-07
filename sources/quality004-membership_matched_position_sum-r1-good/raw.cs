public static class Candidate {
public static long Run(int[] left, int[] right)
{
    var set = new HashSet<int>(right);
    long sum = 0;
    for (int i = 0; i < left.Length; i++)
        if (set.Contains(left[i]))
            sum += (long)i + 1;
    return sum;
}
}