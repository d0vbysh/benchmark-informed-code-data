public static class Candidate {
public static long Run(int[] left, int[] right)
{
    long sum = 0;
    for (int i = 0; i < left.Length; i++)
    {
        int l = left[i];
        if ((l & 1) != 0) // odd
        {
            bool found = false;
            for (int j = 0; j < right.Length; j++)
            {
                if (right[j] == l)
                {
                    found = true;
                    break;
                }
            }
            if (!found) sum += l;
        }
    }
    return sum;
}
}