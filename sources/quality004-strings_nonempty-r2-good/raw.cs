public static class Candidate {
public static string Run(string[] parts)
{
    var sb = new StringBuilder();
    foreach (var p in parts)
        if (p.Length > 0)
            sb.Append(p);
    return sb.ToString();
}
}