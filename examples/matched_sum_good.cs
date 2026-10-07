public static class Candidate {
public static long Run(int[] left, int[] right)
{
    var set = new System.Collections.Generic.HashSet<int>(right); // one hash table
    long sum = 0;
    for (int i = 0; i < left.Length; i++)
        if (set.Contains(left[i]))
            sum += left[i];
    return sum;
}
}