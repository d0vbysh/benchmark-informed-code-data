public static class Candidate {
public static long Run(int[] values, int threshold)
{
    var selected = new List<int>(values.Length);
    foreach (var value in values)
        if ((uint)value > (uint)threshold && value % 2 == 0)
            selected.Add(value);
    return selected.ToArray().Sum(); // one ToArray, one Sum
}
}