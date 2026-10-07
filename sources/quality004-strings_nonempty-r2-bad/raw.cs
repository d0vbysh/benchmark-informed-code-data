public static class Candidate {
public static string Run(string[] parts)
{
    var result = string.Empty;
    foreach (var part in parts)
        if (!string.IsNullOrEmpty(part))
            result += part;
    return result;
}
}