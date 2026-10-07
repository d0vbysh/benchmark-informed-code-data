public static class Candidate {
public static long Run(int[] values, int threshold)
{
    var candidates = new List<int>(values.Length);
    foreach (var value in values)
        if (value >= threshold)
            candidates.Add(value);
    return candidates.ToArray().Sum(); // one ToArray and one Sum
}
}