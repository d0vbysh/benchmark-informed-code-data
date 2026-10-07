public static class Candidate {
public static string Run(string[] parts)
{
    var sb = new System.Text.StringBuilder(parts.Length * 4); // ~4 bytes per char plus ~4 bytes per separator
    foreach (var p in parts)
    {
        if (sb.Length > 0) sb.Append('|'); // no leading separator
        sb.Append(p); // no intermediate strings
    }
    return sb.ToString();
}
}