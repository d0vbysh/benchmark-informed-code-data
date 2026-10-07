public static class Candidate {
public static string Run(string[] parts)
{
    string result = string.Empty;
    foreach (string part in parts)
        result += "[" + part + "]";
    return result;
}
}