public static class Candidate {
public static long Run(int[] values, int threshold)
{
    System.Collections.Generic.List<int> selected = new(values.Length);
    foreach (int value in values)
        if ((long)value > threshold && value % 2 == 0)
            selected.Add(value);
    return selected.ToArray().Sum(); // one allocation; one scan
}
}