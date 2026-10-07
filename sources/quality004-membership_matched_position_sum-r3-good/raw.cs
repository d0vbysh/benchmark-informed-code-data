public static class Candidate {
public static long Run(int[] left, int[] right) // one-pass over left, one HashSet
{
    var set = new HashSet<int>(right); // O(right.Length) construction
    long sum = 0;
    for (int i = 0; i < left.Length; ++i) // O(left.Length) visit
        if (set.Contains(left[i])) sum += i + 1;
    return sum;
}
}