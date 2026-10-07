public static class Candidate {
public static long Run(int[] left, int[] right)
{
    long sum = 0;
    for (int i = 0; i < left.Length; i++)
    {
        int l = left[i];
        if ((l & 1) == 1)
        {
            int j;
            for (j = 0; j < right.Length; j++)
                if (right[j] == l)
                    break;
            if (j == right.Length)
                sum += l;
        }
    }
    return sum;
}
}