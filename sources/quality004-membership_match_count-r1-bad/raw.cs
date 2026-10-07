public static class Candidate {
public static long Run(int[] left, int[] right)
{
    long count = 0;
    foreach (var l in left) // O(L * R)
        foreach (var r in right) // nested loop
            if (l == r) { count++; break; }
    return count;
}
}