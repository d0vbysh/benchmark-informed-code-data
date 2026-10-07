public static class Candidate {
public static string Run(string[] parts)
{
    string result = string.Empty;
    for (int i = 0; i < parts.Length; i++)
    {
        if (i > 0) result += "|"; // repeat delimiter between each element
        result += parts[i];
    }
    return result;
}
}