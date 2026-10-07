public static class Candidate {
public static long Run(int[] values, int threshold)
{
    var selected = new System.Collections.Generic.List<int>(values.Length); // O(n)
    foreach (var v in values) if (v < threshold) selected.Add(v);
    return selected.ToArray().Sum(); // O(n)
}
}