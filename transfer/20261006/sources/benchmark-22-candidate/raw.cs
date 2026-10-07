public static class Candidate {
public static string Run(string[] parts)
{
    var sb = new System.Text.StringBuilder();
    foreach (var p in parts)
        sb.Append(p, p); // Append a string twice
    return sb.ToString();
}
}