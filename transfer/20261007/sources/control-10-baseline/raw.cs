public static class Candidate {
public static long Run(int[] values, int threshold)
{
    var list = new System.Collections.Generic.List<int>(values.Length); // one allocation
    for (int i = 0; i < values.Length; i++)
        if (values[i] > threshold)
            list.Add(i); // one element-wise operation and one allocation per element
    var selected = list.ToArray(); // one allocation
    long sum = 0;
    foreach (var i in selected) // one element-wise operation
        sum += (long)values[i] * values[i];
    return sum;
}
}