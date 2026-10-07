public static class Candidate {
public static long Run(int[] left, int[] right)
{
    System.Collections.Generic.HashSet<int> rightSet = new(right); // one hash table
    long sum = 0;
    foreach (int l in left) // one foreach
        if (rightSet.Contains(l)) // one hash lookup per left
            sum += l;
    return sum;
}
}