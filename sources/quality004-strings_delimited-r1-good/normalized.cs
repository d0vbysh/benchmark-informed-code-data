public static class Candidate {
public static string Run(string[] parts)
{
    var sb = new System.Text.StringBuilder(parts.Length * 4);
    foreach (var part in parts)
    {
        if (sb.Length > 0) sb.Append('|
        sb.Append(part);
    }
    return sb.ToString();
}
}