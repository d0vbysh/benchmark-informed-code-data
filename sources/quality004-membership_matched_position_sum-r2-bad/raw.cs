public static class Candidate {
public static long Run(int[] left, int[] right) // O(n²·m) nested scans
{
    long sum = 0;
    for (int i = 0; i < left.Length; i++)
    {
        bool found = false;
        for (int j = 0; j < right.Length; j++)
            if (left[i] == right[j]) { found = true; break; }
        if (found) sum += (long)i + 1;
    }
    return sum;
}
}