public static class Candidate {
public static string Run(string[] parts)
{
    string result = String.Empty;
    for (int i = parts.Length - 1; i >= 0; i--)
        result += parts[i]; // allocates a new string each time
    return result;
}
}