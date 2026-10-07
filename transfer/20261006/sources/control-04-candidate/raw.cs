public static class Candidate {
public static long Run(int[] left, int[] right)
{
    var set = new System.Collections.Generic.HashSet<int>(right); // one hash table
    long count = 0;
    foreach (var x in left)
        if (set.Contains(x) && x % 2 == 0)
            count++;
    return count;
}
}