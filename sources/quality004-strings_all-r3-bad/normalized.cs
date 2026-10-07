public static class Candidate {
public static string Run(string[] parts) // original signature
{
    string result = string.Empty;
    foreach (string part in parts)
        result += part; // allocates a new string every iteration
    return result;
}
}