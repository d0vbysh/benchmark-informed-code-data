public static class Candidate {
public static long Run(int[] values, int threshold)
{
    var list = new System.Collections.Generic.List<int>(values.Length); // allocate array
    foreach (var value in values) // N iterations
        if (value <= threshold) // signed comparison
            list.Add(value); // add to list
    var selected = list.ToArray(); // allocate array
    long count = 0;
    foreach (var _ in selected) // N iterations
        count++;
    return count;
}
}