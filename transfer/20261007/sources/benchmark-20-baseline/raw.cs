public static class Candidate {
public static string Run(string[] parts)
{
    string result = string.Empty;
    foreach (var part in parts)
        result += part; // O(n^2) string growth
    return result;
}
}