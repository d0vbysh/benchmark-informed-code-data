public static class Candidate {
public static long Run(int[] left, int[] right)
{
    long sum = 0;
    for (int i = 0; i < left.Length; i++)
    {
        int x = left[i];
        if (x % 2 == 0 || Array.Exists(right, y => y == x))
            continue;
        sum += x;
    }
    return sum;
}
}