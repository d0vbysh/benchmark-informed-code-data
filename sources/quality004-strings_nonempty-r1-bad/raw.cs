public static class Candidate {
public static string Run(string[] parts)
{
    var sb = new StringBuilder();
    foreach (var p in parts)
        if (!string.IsNullOrEmpty(p))
            sb.Append(p);
    return sb.ToString();
}
}