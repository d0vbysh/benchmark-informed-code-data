public static class Candidate {
public static string Run(string[] parts)
{
    string result = string.Empty;
    foreach (var p in parts)
        result += p; // each += allocates a new string and copies both
    return result;
}
}