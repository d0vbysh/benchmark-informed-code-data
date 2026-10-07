public static class Candidate {
public static long Run(int[] left, int[] right)
{
    long count = 0;
    foreach (int l in left) // O(left * right)
        foreach (int r in right)
            if (l == r) { count++; break; }
    return count;
}
}