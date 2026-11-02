using System.Diagnostics;
using System.Runtime.Serialization.Formatters;

Console.WriteLine("=== Synchronous Processing ===");

var syncWatch = Stopwatch.StartNew();

Console.WriteLine(ProcessData("File 1"));
Console.WriteLine(ProcessData("File 2"));
Console.WriteLine(ProcessData("File 3"));

syncWatch.Stop();

Console.WriteLine(
    $"Synchronous time: {syncWatch.Elapsed.TotalSeconds:F2} seconds");

Console.WriteLine();

Console.WriteLine("=== Asynchronous Processing ===");

var asyncWatch = Stopwatch.StartNew();

var tasks = new List<Task<string>>
{
    ProcessDataAsync("File 1", 3000),
    ProcessDataAsync("File 2", 1000),
    ProcessDataAsync("File 3", 2000)
};

await Task.WhenAll(tasks);
foreach(var res in tasks)
    Console.WriteLine(res.Result);


asyncWatch.Stop();

Console.WriteLine(
    $"Asynchronous time: {asyncWatch.Elapsed.TotalSeconds:F2} seconds");

Console.WriteLine();


Console.WriteLine("=== Asynchronous Processing 2(WhenAny) ===");
 asyncWatch = Stopwatch.StartNew();

var tasks2 = new List<Task<string>>
{
    ProcessDataAsync("File 1", 3000),
    ProcessDataAsync("File 2", 1000),
    ProcessDataAsync("File 3", 2000)
};

while (tasks2.Count > 0)
{
    var completedTask =await Task.WhenAny(tasks2);
    string completed = await completedTask;

    Console.WriteLine(completed);
    tasks2.Remove(completedTask);

}

asyncWatch.Stop();

Console.WriteLine(
    $"Asynchronous time: {asyncWatch.Elapsed.TotalSeconds:F2} seconds");



static string ProcessData(string dataName)
{
    var syncWatchFile = Stopwatch.StartNew();
    Console.WriteLine($"Start of '{dataName}' processing");
    Thread.Sleep(3000);
    syncWatchFile.Stop();
    return $"ProProcessing '{dataName}' completed in {syncWatchFile.Elapsed.TotalSeconds:F2} seconds.";

}

static async Task<string> ProcessDataAsync(
    string dataName,
    int delayMilliseconds)
{
    var asyncWatchFile = Stopwatch.StartNew();
    Console.WriteLine($"Start of '{dataName}' processing");
     await Task.Delay(delayMilliseconds);
    asyncWatchFile.Stop();
    return $"ProProcessing '{dataName}' completed in {asyncWatchFile.Elapsed.TotalSeconds:F2} seconds.";
}