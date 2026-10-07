public static class Candidate {
public static long Run(int[] left, int[] right)
{
    HashSet<int> rightSet = new HashSet<int>(right);
    long count = 0;
    foreach (int x in left)
        if (rightSet.Contains(x)) count++;
    return count;
}
}