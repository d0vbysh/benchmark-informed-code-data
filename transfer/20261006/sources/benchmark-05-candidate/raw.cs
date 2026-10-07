public static class Candidate {
public static long Run(int[] left, int[] right)
{
    var set = new System.Collections.Generic.HashSet<int>(right); // one allocation
    long count = 0;
    foreach (int l in left)
        if (l % 2 == 0 && set.Contains(l))
            count++;
    return count;
}
}