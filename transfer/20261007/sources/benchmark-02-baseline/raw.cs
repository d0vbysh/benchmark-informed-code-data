public static class Candidate {
public static long Run(int[] left, int[] right)
{
    long sum = 0;
    for (int i = 0; i < left.Length; i++)
    {
        bool found = false;
        for (int j = 0; j < right.Length; j++)
        {
            if (right[j] == left[i])
            {
                found = true;
                break; // first match is enough
            }
        }
        if (!found) sum += i + 1;
    }
    return sum;
}
}