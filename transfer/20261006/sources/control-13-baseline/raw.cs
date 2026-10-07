public static class Candidate {
public static long Run(int[] values, int threshold)
{
    var selected = new System.Collections.Generic.List<int>(values.Length);
    for (int i = 0; i < values.Length; i++)
        if (values[i] < threshold)
            selected.Add(values[i]);
    return selected.Select(x => (long)Math.Abs(x)).Sum();
}
}