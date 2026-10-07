public static class Candidate {
public static string Run(string[] parts)
{
    var sb = new System.Text.StringBuilder();
    for (int i = 0; i < parts.Length; i += 2)
        sb.Append(parts[i]);
    return sb.ToString();
}
}