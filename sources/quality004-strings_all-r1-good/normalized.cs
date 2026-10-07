public static class Candidate {
public static string Run(string[] parts) // one StringBuilder, one Append per part
{
    var sb = new System.Text.StringBuilder(parts.Length * 8); // guess a capacity near the sum
    foreach (var p in parts) sb.Append(p);
    return sb.ToString();
}
}