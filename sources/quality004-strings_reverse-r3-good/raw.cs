public static class Candidate {
public static string Run(string[] parts)
{
    var sb = new StringBuilder(parts.Length * 20);
    for (int i = parts.Length - 1; i >= 0; --i)
        sb.Append(parts[i]);
    return sb.ToString();
}
}