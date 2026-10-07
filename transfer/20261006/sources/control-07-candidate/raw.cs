public static class Candidate {
public static long Run(int[] left, int[] right)
{
    var set = new System.Collections.Generic.HashSet<int>(right); // one allocation
    long sum = 0;
    foreach (var x in left)
        if (x % 2 != 0 && !set.Contains(x))
            sum += x;
    return sum;
}
}