public static class Candidate {
public static long Run(int[] left, int[] right)
{
    System.Collections.Generic.HashSet<int> rightSet = new System.Collections.Generic.HashSet<int>(right);
    long count = 0;
    foreach (int l in left) if (rightSet.Contains(l)) count++;
    return count;
}
}