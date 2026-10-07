public static class Candidate {
public static long Run(int[] left, int[] right)
{
    long count = 0;
    for (int i = 0; i < left.Length; i++)
    {
        int x = left[i];
        if (x % 2 == 0)
        {
            int j;
            for (j = 0; j < right.Length; j++)
                if (right[j] == x) break;
            if (j < right.Length) count++;
        }
    }
    return count;
}
}