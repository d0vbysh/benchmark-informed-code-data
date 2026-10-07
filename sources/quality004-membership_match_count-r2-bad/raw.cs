public static class Candidate {
public static long Run(int[] left, int[] right)
{
    long count = 0;
    foreach (int l in left)
        foreach (int r in right) // O(n*m) scan per left element
            if (l == r) { count++; break; }
    return count;
}
}