public static class Candidate {
public static long Run(int[] values, int threshold)
{
    var selected = new System.Collections.Generic.List<int>(values.Length);
    foreach (var value in values) if (value > threshold) selected.Add(value);
    return selected.ToArray().Sum();
}
}