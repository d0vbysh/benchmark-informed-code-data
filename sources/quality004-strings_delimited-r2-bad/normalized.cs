public static class Candidate {
public static string Run(string[] parts)
{
    string result = string.Empty;
    foreach (var p in parts)
    {
        result += result.Length > 0 ? "|" + p : p; // repeated += on every element
    }
    return result;
}
}