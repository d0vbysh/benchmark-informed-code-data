public static class Candidate {
public static string Run(string[] parts)
{
    var sb = new StringBuilder();
    foreach (var part in parts)
        if (part.Length > 0)
            sb.Append(part);
    return sb.ToString();
}
}