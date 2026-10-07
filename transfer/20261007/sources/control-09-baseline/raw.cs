public static class Candidate {
public static long Run(int[] values, int threshold)
{
    var selected = new System.Collections.Generic.List<int>(values.Length);
    for (int i = 0; i < values.Length; i++)
        if (values[i] <= threshold)
            selected.Add(i);
    var selectedValues = selected.ToArray();
    long count = 0;
    foreach (var value in selectedValues)
        count++;
    return count;
}
}