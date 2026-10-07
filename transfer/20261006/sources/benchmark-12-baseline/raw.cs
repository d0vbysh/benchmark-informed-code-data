public static class Candidate {
public static long Run(int[] values, int threshold)
{
    var list = new System.Collections.Generic.List<int>(values.Length);
    for (int i = 0; i < values.Length; i++)
        if (values[i] < threshold)
            list.Add(values[i]);
    var selected = list.ToArray();
    long sum = 0;
    foreach (var value in selected)
        sum += Math.Abs((long)value);
    return sum;
}
}