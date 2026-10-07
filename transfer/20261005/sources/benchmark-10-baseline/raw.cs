public static class Candidate {
public static long Run(int[] values, int threshold)
{
    var list = new System.Collections.Generic.List<int>(values.Length);
    foreach (var value in values)
        if (value > threshold)
            list.Add(value);
    var array = list.ToArray();
    long sum = 0;
    foreach (var value in array)
        sum += (long)value * value;
    return sum;
}
}