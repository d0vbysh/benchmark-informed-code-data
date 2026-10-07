public static class Candidate {
public static long Run(int[] left, int[] right) // O(n * m) per call
{
    long sum = 0;
    foreach (int l in left)
    {
        bool found = false;
        foreach (int r in right) // nested O(n * m)
        {
            if (r == l)
            {
                found = true;
                break;
            }
        }
        if (found) sum += l;
    }
    return sum;
}
}