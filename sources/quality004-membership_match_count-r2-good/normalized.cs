public static class Candidate {
public static long Run(int[] left, int[] right)
{
    System.Collections.Generic.HashSet<int> inRight = new System.Collections.Generic.HashSet<int>(right); // one allocation
    long count = 0;
    foreach (int x in left) // one enumeration
        if (inRight.Contains(x))
            count++;
    return count;
}
}