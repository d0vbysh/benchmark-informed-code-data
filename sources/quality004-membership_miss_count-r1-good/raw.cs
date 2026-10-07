public static class Candidate {
public static long Run(int[] left, int[] right)
{
    HashSet<int> rightSet = new(right); // one hash table
    long count = 0;
    foreach (int x in left)
        if (!rightSet.Contains(x)) count++;
    return count;
}
}