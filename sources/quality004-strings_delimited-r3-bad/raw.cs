public static class Candidate {
public static string Run(string[] parts)
{
    string result = string.Empty;
    foreach (var p in parts)
    {
        if (result.Length > 0) result += "|";
        result += p;
    }
    return result;
}
}