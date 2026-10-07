public static class Candidate {
public static string Run(string[] parts)
{
    string result = string.Empty;
    foreach (string p in parts)
        result += p + p;
    return result;
}
}