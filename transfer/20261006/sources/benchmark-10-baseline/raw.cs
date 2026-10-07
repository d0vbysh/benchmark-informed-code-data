public static class Candidate {
public static long Run(int[] values, int threshold)
{
    var list = new System.Collections.Generic.List<int>(values.Length);
    for (int i = 0; i < values.Length; i++)
        if (values[i] > threshold)
            list.Add(i);
    var indices = list.ToArray();
    long sum = 0;
    foreach (int i in indices)
        sum += (long)values[i] * values[i];
    return sum;
}
}