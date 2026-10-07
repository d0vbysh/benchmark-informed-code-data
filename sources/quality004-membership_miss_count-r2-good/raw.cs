public static class Candidate {
public static long Run(int[] left, int[] right)
{
    var set = new HashSet<int>(right); // one hash table
    long missing = 0;
    foreach (int l in left) // one probe per element
        if (!set.Contains(l)) missing++;
    return missing;
}
}