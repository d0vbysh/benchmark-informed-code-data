public static class Candidate {
public static long Run(int[] left, int[] right)
{
    var set = new System.Collections.Generic.HashSet<int>(right); // one allocation
    long sum = 0;
    foreach (int x in left) // one hash lookup per left element
        if (set.Contains(x))
            sum += (long)x * x;
    return sum;
}
}