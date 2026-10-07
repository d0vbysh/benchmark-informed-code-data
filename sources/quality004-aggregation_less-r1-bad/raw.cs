public static class Candidate {
public static long Run(int[] values, int threshold)
{
    List<int> selected = new(values.Length); // O(n) allocation
    foreach (int value in values) // one pass
        if (value < threshold)
            selected.Add(value);
    return selected.ToArray().Sum(); // one pass + one ToArray
}
}