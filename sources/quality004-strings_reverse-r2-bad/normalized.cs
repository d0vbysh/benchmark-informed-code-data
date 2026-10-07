public static class Candidate {
public static string Run(string[] parts)
{
    string result = string.Empty;
    for (int i = parts.Length - 1; i >= 0; i--)
        result += parts[i];
    return result;
}
}