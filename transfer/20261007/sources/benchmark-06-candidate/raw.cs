public static class Candidate {
public static long Run(int[] left, int[] right)
{
    var set = new System.Collections.Generic.HashSet<int>(right); // one hash table
    long sum = 0;
    foreach (var l in left) // one probe per left value
        if (l % 2 != 0 && !set.Contains(l))
            sum += l;
    return sum;
}
}