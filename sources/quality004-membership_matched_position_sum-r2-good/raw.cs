public static class Candidate {
public static long Run(int[] left, int[] right)
{
    var set = new HashSet<int>(right); // O(right.Length)
    long sum = 0;
    for (int i = 0; i < left.Length; i++) // O(left.Length)
        if (set.Contains(left[i]))
            sum += i + 1;
    return sum;
}
}