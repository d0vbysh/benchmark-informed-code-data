public static class Candidate {
public static long Run(int[] left, int[] right)
{
    var set = new HashSet<int>(right); // one allocation, one pass over right
    long count = 0;
    foreach (var x in left) // one pass over left
        if (!set.Contains(x)) count++;
    return count;
}
}