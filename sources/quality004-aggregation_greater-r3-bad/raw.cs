public static class Candidate {
public static long Run(int[] values, int threshold)
{
    List<int> selected = new(values.Length);
    for (int i = 0; i < values.Length; ++i)
        if (values[i] > threshold)
            selected.Add(i);
    return selected.Select(i => (long)values[i]).Sum();
}
}