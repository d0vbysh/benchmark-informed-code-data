public static class Candidate {
public static long Run(int[] left, int[] right) // one hash set from right
{
    System.Collections.Generic.HashSet<int> rightSet = new(right); // one hash table
    long evenCount = 0;
    foreach (int l in left)
        if (l % 2 == 0 && rightSet.Contains(l))
            evenCount++;
    return evenCount;
}
}