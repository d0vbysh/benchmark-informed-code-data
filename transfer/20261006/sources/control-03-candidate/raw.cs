public static class Candidate {
public static long Run(int[] left, int[] right)
{
    var set = new System.Collections.Generic.HashSet<int>(right); // one allocation
    long sum = 0;
    for (int i = 0; i < left.Length; i++)
        if (!set.Contains(left[i]))
            sum += i + 1;
    return sum;
}
}