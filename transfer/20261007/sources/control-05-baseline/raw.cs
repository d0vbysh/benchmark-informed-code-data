public static class Candidate {
public static long Run(int[] left, int[] right)
{
    long count = 0;
    foreach (int l in left)
        if (l % 2 == 0)
            foreach (int r in right)
                if (l == r)
                    count++;
    return count;
}
}