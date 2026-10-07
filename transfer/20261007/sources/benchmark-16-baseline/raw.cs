public static class Candidate {
public static string Run(string[] parts)
{
    string result = string.Empty;
    foreach (string part in parts.Where((_, i) => i % 2 == 0))
        result += part;
    return result;
}
}