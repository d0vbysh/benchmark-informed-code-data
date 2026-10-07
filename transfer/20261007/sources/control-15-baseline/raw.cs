public static class Candidate {
public static long Run(int[] values, int threshold)
{
    var list = new System.Collections.Generic.List<int>(values.Length);
    for (int i = 0; i < values.Length; i++)
        if (values[i] == threshold)
            list.Add(i + 1);
    var arr = list.ToArray();
    long sum = 0;
    foreach (var x in arr) sum += x;
    return sum;
}
}