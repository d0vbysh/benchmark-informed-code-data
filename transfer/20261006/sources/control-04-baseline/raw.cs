public static class Candidate {
public static long Run(int[] left, int[] right)
{
    long count = 0;
    for (int i = 0; i < left.Length; i++)
        for (int j = 0; j < right.Length; j++)
            if (left[i] == right[j])
            {
                if (left[i] % 2 == 0)
                    count++;
                break;
            }
    return count;
}
}