public static class Candidate {
public static string Run(string[] parts) // one StringBuilder, one Append, one ToString
{
    var sb = new System.Text.StringBuilder(parts.Length * 16); // initial capacity guess
    foreach (var p in parts)
        sb.Append("[", p, "]");
    return sb.ToString();
}
}