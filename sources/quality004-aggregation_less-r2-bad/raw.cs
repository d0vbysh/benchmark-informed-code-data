public static class Candidate {
public static long Run(int[] values, int threshold)
{
    var indices = new List<int>(values.Length);
    for (int i = 0; i < values.Length; i++)
        if (values[i] < threshold)
            indices.Add(i);
    var selected = indices.ToArray();
    long sum = 0;
    foreach (var i in selected)
        sum += values[i];
    return sum;
}
}