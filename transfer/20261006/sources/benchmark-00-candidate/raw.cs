public static class Candidate {
public static long Run(int[] left, int[] right)
{
    var set = new System.Collections.Generic.HashSet<int>(right); // one hash table
    long sum = 0;
    foreach (int x in left) // one probe per left element
        if (set.Contains(x))
            sum += (long)x * x;
    return sum;
}
}