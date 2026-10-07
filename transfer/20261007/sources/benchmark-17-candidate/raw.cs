public static class Candidate {
public static string Run(string[] parts)
{
    var sb = new System.Text.StringBuilder(parts.Length * 16); // 16 is an overestimate, but avoids reallocation
    for (int i = 0; i < parts.Length; i += 2)
        sb.Append(parts[i]);
    return sb.ToString();
}
}