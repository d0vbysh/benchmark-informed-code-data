public static class Candidate {
public static long Run(int[] left, int[] right)
{
    System.Collections.Generic.HashSet<int> set = new(right); // one hash table
    long count = 0;
    foreach (int x in left)
        if (x % 2 == 0 && set.Contains(x))
            count++;
    return count;
}
}