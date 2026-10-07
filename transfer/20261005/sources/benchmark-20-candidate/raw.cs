public static class Candidate {
public static string Run(string[] parts)
{
    var sb = new System.Text.StringBuilder(parts.Length * 16);
    foreach (var p in parts)
        sb.Append(p);
    return sb.ToString();
}
}