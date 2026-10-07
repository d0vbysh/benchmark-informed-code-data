public static class Candidate {
public static string Run(string[] parts)
{
    var result = string.Empty;
    foreach (var p in parts.Reverse())
        result += p;
    return result;
}
}