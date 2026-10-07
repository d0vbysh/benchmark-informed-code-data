public static class Candidate {
public static long Run(int[] left, int[] right)
{
    System.Collections.Generic.HashSet<int> set = new(right); // one hash table
    long sum = 0;
    for (int i = 0; i < left.Length; i++)
        if (!set.Contains(left[i])) sum += i + 1;
    return sum;
}
}