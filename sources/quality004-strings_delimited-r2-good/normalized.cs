public static class Candidate {
public static string Run(string[] parts)
{
    var sb = new System.Text.StringBuilder(parts.Length * 16);
    for (int i = 0; i < parts.Length; i++)
    {
        if (i > 0) sb.Append('|');
        sb.Append(parts[i]);
    }
    return sb.ToString();
}
}