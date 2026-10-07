public static class Candidate {
public static long Run(int[] values, int threshold)
{
    var list = new System.Collections.Generic.List<int>(values.Length); // O(n)
    for (int i = 0; i < values.Length; i++)
        if (values[i] < threshold)
            list.Add(values[i]);
    return list.Select(x => (long)Math.Abs(x)).Sum(); // O(n)
}
}