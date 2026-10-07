using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters.Json;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Perfolizer.Mathematics.OutlierDetection;
namespace Quality004;
public static class Input {
 public static JsonElement Load(string id) {
  if(Environment.Version.ToString()!="8.0.31" || !Environment.Is64BitProcess) throw new Exception("runtime");
  var job=JsonDocument.Parse(File.ReadAllText(Environment.GetEnvironmentVariable("BQM_JOB")!)).RootElement;
  if(job.GetProperty("attempt_id").GetString()!=id) throw new Exception("identity");
  return job;
 }
 public static void Verify(JsonElement job, object actual, object input) {
  var expected=job.GetProperty("expected");
  object wanted=expected.ValueKind==JsonValueKind.String?expected.GetString()!:(object)expected.GetInt64();
  if(!Equals(actual,wanted)) throw new Exception("actual input correctness");
  if(JsonSerializer.Serialize(input)!=JsonSerializer.Serialize(job.GetProperty("input").GetProperty("input"))) throw new Exception("input mutation or mismatch");
  File.WriteAllText(Environment.GetEnvironmentVariable("BQM_META")!,JsonSerializer.Serialize(new{job_id=job.GetProperty("job_id").GetString(),runtime=Environment.Version.ToString(),input,actual,correctness="pass"}));
 }
}
public static class Entry {
 public static void Main(string[] args) {
  var job=Job.Default.WithLaunchCount(1).WithWarmupCount(3).WithIterationCount(5).WithOutlierMode(OutlierMode.DontRemove);
  var config=ManualConfig.Create(DefaultConfig.Instance).AddJob(job).AddExporter(JsonExporter.Full);
  BenchmarkSwitcher.FromAssembly(typeof(Entry).Assembly).Run(args,config);
 }
}

public static class CodeSample00 {
public static long Run(int[] values, int threshold)
{
    var list = new System.Collections.Generic.List<int>(values.Length);
    for (int i = 0; i < values.Length; i++)
        if (values[i] >= threshold)
            list.Add(i);
    var indices = list.ToArray();
    long sum = 0;
    foreach (var i in indices)
        sum += values[i];
    return sum;
}
}
[MemoryDiagnoser] public class Sample00 {
 int[] values=null!;int threshold;
 [GlobalSetup] public void Setup() {var job=Input.Load("quality004-aggregation_greater_equal-r1-bad");var x=job.GetProperty("input").GetProperty("input");values=x.GetProperty("values").Deserialize<int[]>()!;threshold=x.GetProperty("threshold").GetInt32(); Input.Verify(job,CodeSample00.Run(values,threshold),new{values,threshold});}
 [Benchmark] public long Run()=>CodeSample00.Run(values,threshold);
}

public static class CodeSample01 {
public static long Run(int[] values, int threshold)
{
    long sum = 0;
    foreach (int value in values)
        if (value >= threshold)
            sum += value;
    return sum;
}
}
[MemoryDiagnoser] public class Sample01 {
 int[] values=null!;int threshold;
 [GlobalSetup] public void Setup() {var job=Input.Load("quality004-aggregation_greater_equal-r1-good");var x=job.GetProperty("input").GetProperty("input");values=x.GetProperty("values").Deserialize<int[]>()!;threshold=x.GetProperty("threshold").GetInt32(); Input.Verify(job,CodeSample01.Run(values,threshold),new{values,threshold});}
 [Benchmark] public long Run()=>CodeSample01.Run(values,threshold);
}

public static class CodeSample02 {
public static long Run(int[] left, int[] right)
{
    long sum = 0;
    for (int i = 0; i < left.Length; i++)
    {
        bool found = false;
        for (int j = 0; j < right.Length; j++)
        {
            if (right[j] == left[i])
            {
                found = true;
                break;
            }
        }
        if (found) sum += (long)i + 1;
    }
    return sum;
}
}
[MemoryDiagnoser] public class Sample02 {
 int[] left=null!,right=null!;
 [GlobalSetup] public void Setup() {var job=Input.Load("quality004-membership_matched_position_sum-r1-bad");var x=job.GetProperty("input").GetProperty("input");left=x.GetProperty("left").Deserialize<int[]>()!;right=x.GetProperty("right").Deserialize<int[]>()!; Input.Verify(job,CodeSample02.Run(left,right),new{left,right});}
 [Benchmark] public long Run()=>CodeSample02.Run(left,right);
}

public static class CodeSample03 {
public static long Run(int[] left, int[] right)
{
    var set = new System.Collections.Generic.HashSet<int>(right);
    long sum = 0;
    for (int i = 0; i < left.Length; i++)
        if (set.Contains(left[i]))
            sum += (long)i + 1;
    return sum;
}
}
[MemoryDiagnoser] public class Sample03 {
 int[] left=null!,right=null!;
 [GlobalSetup] public void Setup() {var job=Input.Load("quality004-membership_matched_position_sum-r1-good");var x=job.GetProperty("input").GetProperty("input");left=x.GetProperty("left").Deserialize<int[]>()!;right=x.GetProperty("right").Deserialize<int[]>()!; Input.Verify(job,CodeSample03.Run(left,right),new{left,right});}
 [Benchmark] public long Run()=>CodeSample03.Run(left,right);
}

public static class CodeSample04 {
public static long Run(int[] left, int[] right) // O(n²·m) nested scans
{
    long sum = 0;
    for (int i = 0; i < left.Length; i++)
    {
        bool found = false;
        for (int j = 0; j < right.Length; j++)
            if (left[i] == right[j]) { found = true; break; }
        if (found) sum += (long)i + 1;
    }
    return sum;
}
}
[MemoryDiagnoser] public class Sample04 {
 int[] left=null!,right=null!;
 [GlobalSetup] public void Setup() {var job=Input.Load("quality004-membership_matched_position_sum-r2-bad");var x=job.GetProperty("input").GetProperty("input");left=x.GetProperty("left").Deserialize<int[]>()!;right=x.GetProperty("right").Deserialize<int[]>()!; Input.Verify(job,CodeSample04.Run(left,right),new{left,right});}
 [Benchmark] public long Run()=>CodeSample04.Run(left,right);
}

public static class CodeSample05 {
public static long Run(int[] left, int[] right)
{
    var set = new System.Collections.Generic.HashSet<int>(right); // O(right.Length)
    long sum = 0;
    for (int i = 0; i < left.Length; i++) // O(left.Length)
        if (set.Contains(left[i]))
            sum += i + 1;
    return sum;
}
}
[MemoryDiagnoser] public class Sample05 {
 int[] left=null!,right=null!;
 [GlobalSetup] public void Setup() {var job=Input.Load("quality004-membership_matched_position_sum-r2-good");var x=job.GetProperty("input").GetProperty("input");left=x.GetProperty("left").Deserialize<int[]>()!;right=x.GetProperty("right").Deserialize<int[]>()!; Input.Verify(job,CodeSample05.Run(left,right),new{left,right});}
 [Benchmark] public long Run()=>CodeSample05.Run(left,right);
}

public static class CodeSample06 {
public static long Run(int[] left, int[] right)
{
    long sum = 0;
    for (int i = 0; i < left.Length; ++i)
    {
        bool found = false;
        for (int j = 0; j < right.Length; ++j)
        {
            if (right[j] == left[i])
            {
                found = true;
                break; // inner loop only
            }
        }
        if (found)
            sum += (long)i + 1;
    }
    return sum;
}
}
[MemoryDiagnoser] public class Sample06 {
 int[] left=null!,right=null!;
 [GlobalSetup] public void Setup() {var job=Input.Load("quality004-membership_matched_position_sum-r3-bad");var x=job.GetProperty("input").GetProperty("input");left=x.GetProperty("left").Deserialize<int[]>()!;right=x.GetProperty("right").Deserialize<int[]>()!; Input.Verify(job,CodeSample06.Run(left,right),new{left,right});}
 [Benchmark] public long Run()=>CodeSample06.Run(left,right);
}

public static class CodeSample07 {
public static long Run(int[] left, int[] right) // one-pass over left, one HashSet
{
    var set = new System.Collections.Generic.HashSet<int>(right); // O(right.Length) construction
    long sum = 0;
    for (int i = 0; i < left.Length; ++i) // O(left.Length) visit
        if (set.Contains(left[i])) sum += i + 1;
    return sum;
}
}
[MemoryDiagnoser] public class Sample07 {
 int[] left=null!,right=null!;
 [GlobalSetup] public void Setup() {var job=Input.Load("quality004-membership_matched_position_sum-r3-good");var x=job.GetProperty("input").GetProperty("input");left=x.GetProperty("left").Deserialize<int[]>()!;right=x.GetProperty("right").Deserialize<int[]>()!; Input.Verify(job,CodeSample07.Run(left,right),new{left,right});}
 [Benchmark] public long Run()=>CodeSample07.Run(left,right);
}

public static class CodeSample08 {
public static long Run(int[] left, int[] right)
{
    long sum = 0;
    for (int i = 0; i < left.Length; i++)
    {
        bool found = false;
        for (int j = 0; j < right.Length; j++)
            if (right[j] == left[i])
            {
                found = true;
                break;
            }
        if (found) sum += (long)left[i];
    }
    return sum;
}
}
[MemoryDiagnoser] public class Sample08 {
 int[] left=null!,right=null!;
 [GlobalSetup] public void Setup() {var job=Input.Load("quality004-membership_matched_sum-r1-bad");var x=job.GetProperty("input").GetProperty("input");left=x.GetProperty("left").Deserialize<int[]>()!;right=x.GetProperty("right").Deserialize<int[]>()!; Input.Verify(job,CodeSample08.Run(left,right),new{left,right});}
 [Benchmark] public long Run()=>CodeSample08.Run(left,right);
}

public static class CodeSample09 {
public static long Run(int[] left, int[] right)
{
    var set = new System.Collections.Generic.HashSet<int>(right); // one hash table
    long sum = 0;
    for (int i = 0; i < left.Length; i++)
        if (set.Contains(left[i]))
            sum += left[i];
    return sum;
}
}
[MemoryDiagnoser] public class Sample09 {
 int[] left=null!,right=null!;
 [GlobalSetup] public void Setup() {var job=Input.Load("quality004-membership_matched_sum-r1-good");var x=job.GetProperty("input").GetProperty("input");left=x.GetProperty("left").Deserialize<int[]>()!;right=x.GetProperty("right").Deserialize<int[]>()!; Input.Verify(job,CodeSample09.Run(left,right),new{left,right});}
 [Benchmark] public long Run()=>CodeSample09.Run(left,right);
}

public static class CodeSample10 {
public static long Run(int[] left, int[] right)
{
    long sum = 0;
    for (int i = 0; i < left.Length; i++)
    {
        bool found = false;
        for (int j = 0; j < right.Length; j++)
        {
            if (left[i] == right[j])
            {
                found = true;
                break;
            }
        }
        if (found) sum += (long)left[i]; // sum may overflow
    }
    return sum;
}
}
[MemoryDiagnoser] public class Sample10 {
 int[] left=null!,right=null!;
 [GlobalSetup] public void Setup() {var job=Input.Load("quality004-membership_matched_sum-r2-bad");var x=job.GetProperty("input").GetProperty("input");left=x.GetProperty("left").Deserialize<int[]>()!;right=x.GetProperty("right").Deserialize<int[]>()!; Input.Verify(job,CodeSample10.Run(left,right),new{left,right});}
 [Benchmark] public long Run()=>CodeSample10.Run(left,right);
}

public static class CodeSample11 {
public static long Run(int[] left, int[] right)
{
    var set = new System.Collections.Generic.HashSet<int>(right); // one allocation, one hash per element
    long sum = 0;
    for (int i = 0; i < left.Length; i++)
        if (set.Contains(left[i]))
            sum += (long)left[i];
    return sum;
}
}
[MemoryDiagnoser] public class Sample11 {
 int[] left=null!,right=null!;
 [GlobalSetup] public void Setup() {var job=Input.Load("quality004-membership_matched_sum-r2-good");var x=job.GetProperty("input").GetProperty("input");left=x.GetProperty("left").Deserialize<int[]>()!;right=x.GetProperty("right").Deserialize<int[]>()!; Input.Verify(job,CodeSample11.Run(left,right),new{left,right});}
 [Benchmark] public long Run()=>CodeSample11.Run(left,right);
}

public static class CodeSample12 {
public static long Run(int[] left, int[] right) // O(n * m) per call
{
    long sum = 0;
    foreach (int l in left)
    {
        bool found = false;
        foreach (int r in right) // nested O(n * m)
        {
            if (r == l)
            {
                found = true;
                break;
            }
        }
        if (found) sum += l;
    }
    return sum;
}
}
[MemoryDiagnoser] public class Sample12 {
 int[] left=null!,right=null!;
 [GlobalSetup] public void Setup() {var job=Input.Load("quality004-membership_matched_sum-r3-bad");var x=job.GetProperty("input").GetProperty("input");left=x.GetProperty("left").Deserialize<int[]>()!;right=x.GetProperty("right").Deserialize<int[]>()!; Input.Verify(job,CodeSample12.Run(left,right),new{left,right});}
 [Benchmark] public long Run()=>CodeSample12.Run(left,right);
}

public static class CodeSample13 {
public static long Run(int[] left, int[] right)
{
    System.Collections.Generic.HashSet<int> rightSet = new(right); // one hash table
    long sum = 0;
    foreach (int l in left) // one foreach
        if (rightSet.Contains(l)) // one hash lookup per left
            sum += l;
    return sum;
}
}
[MemoryDiagnoser] public class Sample13 {
 int[] left=null!,right=null!;
 [GlobalSetup] public void Setup() {var job=Input.Load("quality004-membership_matched_sum-r3-good");var x=job.GetProperty("input").GetProperty("input");left=x.GetProperty("left").Deserialize<int[]>()!;right=x.GetProperty("right").Deserialize<int[]>()!; Input.Verify(job,CodeSample13.Run(left,right),new{left,right});}
 [Benchmark] public long Run()=>CodeSample13.Run(left,right);
}

public static class CodeSample14 {
public static long Run(int[] left, int[] right)
{
    long count = 0;
    for (int i = 0; i < left.Length; i++)
    {
        bool found = false;
        for (int j = 0; j < right.Length; j++)
            if (left[i] == right[j]) { found = true; break; }
        if (!found) count++;
    }
    return count;
}
}
[MemoryDiagnoser] public class Sample14 {
 int[] left=null!,right=null!;
 [GlobalSetup] public void Setup() {var job=Input.Load("quality004-membership_miss_count-r1-bad");var x=job.GetProperty("input").GetProperty("input");left=x.GetProperty("left").Deserialize<int[]>()!;right=x.GetProperty("right").Deserialize<int[]>()!; Input.Verify(job,CodeSample14.Run(left,right),new{left,right});}
 [Benchmark] public long Run()=>CodeSample14.Run(left,right);
}

public static class CodeSample15 {
public static long Run(int[] left, int[] right)
{
    System.Collections.Generic.HashSet<int> rightSet = new(right); // one hash table
    long count = 0;
    foreach (int x in left)
        if (!rightSet.Contains(x)) count++;
    return count;
}
}
[MemoryDiagnoser] public class Sample15 {
 int[] left=null!,right=null!;
 [GlobalSetup] public void Setup() {var job=Input.Load("quality004-membership_miss_count-r1-good");var x=job.GetProperty("input").GetProperty("input");left=x.GetProperty("left").Deserialize<int[]>()!;right=x.GetProperty("right").Deserialize<int[]>()!; Input.Verify(job,CodeSample15.Run(left,right),new{left,right});}
 [Benchmark] public long Run()=>CodeSample15.Run(left,right);
}

public static class CodeSample16 {
public static long Run(int[] left, int[] right)
{
    long count = 0;
    for (int i = 0; i < left.Length; i++)
    {
        bool found = false;
        for (int j = 0; j < right.Length; j++)
            if (right[j] == left[i]) { found = true; break; }
        if (!found) count++;
    }
    return count;
}
}
[MemoryDiagnoser] public class Sample16 {
 int[] left=null!,right=null!;
 [GlobalSetup] public void Setup() {var job=Input.Load("quality004-membership_miss_count-r2-bad");var x=job.GetProperty("input").GetProperty("input");left=x.GetProperty("left").Deserialize<int[]>()!;right=x.GetProperty("right").Deserialize<int[]>()!; Input.Verify(job,CodeSample16.Run(left,right),new{left,right});}
 [Benchmark] public long Run()=>CodeSample16.Run(left,right);
}

public static class CodeSample17 {
public static long Run(int[] left, int[] right)
{
    var set = new System.Collections.Generic.HashSet<int>(right); // one hash table
    long missing = 0;
    foreach (int l in left) // one probe per element
        if (!set.Contains(l)) missing++;
    return missing;
}
}
[MemoryDiagnoser] public class Sample17 {
 int[] left=null!,right=null!;
 [GlobalSetup] public void Setup() {var job=Input.Load("quality004-membership_miss_count-r2-good");var x=job.GetProperty("input").GetProperty("input");left=x.GetProperty("left").Deserialize<int[]>()!;right=x.GetProperty("right").Deserialize<int[]>()!; Input.Verify(job,CodeSample17.Run(left,right),new{left,right});}
 [Benchmark] public long Run()=>CodeSample17.Run(left,right);
}

public static class CodeSample18 {
public static long Run(int[] left, int[] right)
{
    long count = 0;
    for (int i = 0; i < left.Length; i++)
    {
        bool found = false;
        for (int j = 0; j < right.Length; j++)
        {
            if (left[i] == right[j])
            {
                found = true;
                break;
            }
        }
        if (!found) count++;
    }
    return count;
}
}
[MemoryDiagnoser] public class Sample18 {
 int[] left=null!,right=null!;
 [GlobalSetup] public void Setup() {var job=Input.Load("quality004-membership_miss_count-r3-bad");var x=job.GetProperty("input").GetProperty("input");left=x.GetProperty("left").Deserialize<int[]>()!;right=x.GetProperty("right").Deserialize<int[]>()!; Input.Verify(job,CodeSample18.Run(left,right),new{left,right});}
 [Benchmark] public long Run()=>CodeSample18.Run(left,right);
}

public static class CodeSample19 {
public static long Run(int[] left, int[] right)
{
    var set = new System.Collections.Generic.HashSet<int>(right); // one allocation, one pass over right
    long count = 0;
    foreach (var x in left) // one pass over left
        if (!set.Contains(x)) count++;
    return count;
}
}
[MemoryDiagnoser] public class Sample19 {
 int[] left=null!,right=null!;
 [GlobalSetup] public void Setup() {var job=Input.Load("quality004-membership_miss_count-r3-good");var x=job.GetProperty("input").GetProperty("input");left=x.GetProperty("left").Deserialize<int[]>()!;right=x.GetProperty("right").Deserialize<int[]>()!; Input.Verify(job,CodeSample19.Run(left,right),new{left,right});}
 [Benchmark] public long Run()=>CodeSample19.Run(left,right);
}

public static class CodeSample20 {
public static string Run(string[] parts)
{
    string result = string.Empty;
    foreach (var p in parts)
        result += p; // each += allocates a new string and copies both
    return result;
}
}
[MemoryDiagnoser] public class Sample20 {
 string[] parts=null!;
 [GlobalSetup] public void Setup() {var job=Input.Load("quality004-strings_all-r1-bad");var x=job.GetProperty("input").GetProperty("input");parts=x.GetProperty("parts").Deserialize<string[]>()!; Input.Verify(job,CodeSample20.Run(parts),new{parts});}
 [Benchmark] public string Run()=>CodeSample20.Run(parts);
}

public static class CodeSample21 {
public static string Run(string[] parts) // one StringBuilder, one Append per part
{
    var sb = new System.Text.StringBuilder(parts.Length * 8); // guess a capacity near the sum
    foreach (var p in parts) sb.Append(p);
    return sb.ToString();
}
}
[MemoryDiagnoser] public class Sample21 {
 string[] parts=null!;
 [GlobalSetup] public void Setup() {var job=Input.Load("quality004-strings_all-r1-good");var x=job.GetProperty("input").GetProperty("input");parts=x.GetProperty("parts").Deserialize<string[]>()!; Input.Verify(job,CodeSample21.Run(parts),new{parts});}
 [Benchmark] public string Run()=>CodeSample21.Run(parts);
}

public static class CodeSample22 {
public static string Run(string[] parts)
{
    string result = string.Empty;
    foreach (string part in parts)
        result += part;
    return result;
}
}
[MemoryDiagnoser] public class Sample22 {
 string[] parts=null!;
 [GlobalSetup] public void Setup() {var job=Input.Load("quality004-strings_all-r2-bad");var x=job.GetProperty("input").GetProperty("input");parts=x.GetProperty("parts").Deserialize<string[]>()!; Input.Verify(job,CodeSample22.Run(parts),new{parts});}
 [Benchmark] public string Run()=>CodeSample22.Run(parts);
}

public static class CodeSample23 {
public static string Run(string[] parts)
{
    var sb = new System.Text.StringBuilder(parts.Length * 8); // estimate
    foreach (var p in parts)
        sb.Append(p);
    return sb.ToString();
}
}
[MemoryDiagnoser] public class Sample23 {
 string[] parts=null!;
 [GlobalSetup] public void Setup() {var job=Input.Load("quality004-strings_all-r2-good");var x=job.GetProperty("input").GetProperty("input");parts=x.GetProperty("parts").Deserialize<string[]>()!; Input.Verify(job,CodeSample23.Run(parts),new{parts});}
 [Benchmark] public string Run()=>CodeSample23.Run(parts);
}

public static class CodeSample24 {
public static string Run(string[] parts) // original signature
{
    string result = string.Empty;
    foreach (string part in parts)
        result += part; // allocates a new string every iteration
    return result;
}
}
[MemoryDiagnoser] public class Sample24 {
 string[] parts=null!;
 [GlobalSetup] public void Setup() {var job=Input.Load("quality004-strings_all-r3-bad");var x=job.GetProperty("input").GetProperty("input");parts=x.GetProperty("parts").Deserialize<string[]>()!; Input.Verify(job,CodeSample24.Run(parts),new{parts});}
 [Benchmark] public string Run()=>CodeSample24.Run(parts);
}

public static class CodeSample25 {
public static string Run(string[] parts)
{
    var sb = new System.Text.StringBuilder();
    foreach (var p in parts)
        sb.Append(p);
    return sb.ToString();
}
}
[MemoryDiagnoser] public class Sample25 {
 string[] parts=null!;
 [GlobalSetup] public void Setup() {var job=Input.Load("quality004-strings_all-r3-good");var x=job.GetProperty("input").GetProperty("input");parts=x.GetProperty("parts").Deserialize<string[]>()!; Input.Verify(job,CodeSample25.Run(parts),new{parts});}
 [Benchmark] public string Run()=>CodeSample25.Run(parts);
}

public static class CodeSample26 {
public static string Run(string[] parts)
{
    var result = string.Empty;
    foreach (var part in parts)
        if (!string.IsNullOrEmpty(part))
            result += part;
    return result;
}
}
[MemoryDiagnoser] public class Sample26 {
 string[] parts=null!;
 [GlobalSetup] public void Setup() {var job=Input.Load("quality004-strings_nonempty-r2-bad");var x=job.GetProperty("input").GetProperty("input");parts=x.GetProperty("parts").Deserialize<string[]>()!; Input.Verify(job,CodeSample26.Run(parts),new{parts});}
 [Benchmark] public string Run()=>CodeSample26.Run(parts);
}

public static class CodeSample27 {
public static string Run(string[] parts)
{
    var sb = new System.Text.StringBuilder();
    foreach (var p in parts)
        if (p.Length > 0)
            sb.Append(p);
    return sb.ToString();
}
}
[MemoryDiagnoser] public class Sample27 {
 string[] parts=null!;
 [GlobalSetup] public void Setup() {var job=Input.Load("quality004-strings_nonempty-r2-good");var x=job.GetProperty("input").GetProperty("input");parts=x.GetProperty("parts").Deserialize<string[]>()!; Input.Verify(job,CodeSample27.Run(parts),new{parts});}
 [Benchmark] public string Run()=>CodeSample27.Run(parts);
}

public static class CodeSample28 {
public static string Run(string[] parts)
{
    string result = string.Empty;
    for (int i = parts.Length - 1; i >= 0; i--)
        result += parts[i];
    return result;
}
}
[MemoryDiagnoser] public class Sample28 {
 string[] parts=null!;
 [GlobalSetup] public void Setup() {var job=Input.Load("quality004-strings_reverse-r2-bad");var x=job.GetProperty("input").GetProperty("input");parts=x.GetProperty("parts").Deserialize<string[]>()!; Input.Verify(job,CodeSample28.Run(parts),new{parts});}
 [Benchmark] public string Run()=>CodeSample28.Run(parts);
}

public static class CodeSample29 {
public static string Run(string[] parts)
{
    var sb = new System.Text.StringBuilder();
    for (int i = parts.Length - 1; i >= 0; --i)
        sb.Append(parts[i]);
    return sb.ToString();
}
}
[MemoryDiagnoser] public class Sample29 {
 string[] parts=null!;
 [GlobalSetup] public void Setup() {var job=Input.Load("quality004-strings_reverse-r2-good");var x=job.GetProperty("input").GetProperty("input");parts=x.GetProperty("parts").Deserialize<string[]>()!; Input.Verify(job,CodeSample29.Run(parts),new{parts});}
 [Benchmark] public string Run()=>CodeSample29.Run(parts);
}
