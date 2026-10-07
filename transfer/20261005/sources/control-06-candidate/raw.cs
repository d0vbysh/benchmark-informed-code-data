public static class Candidate {
public static long Run(int[] left, int[] right)
{
    var set = new System.Collections.Generic.HashSet<int>(right); // one hash table
    long sum = 0;
    foreach (int l in left)
        if ((l & 1) == 1 && !set.Contains(l))
            sum += l;
    return sum;
}
}