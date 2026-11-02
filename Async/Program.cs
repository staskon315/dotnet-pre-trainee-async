using System.Diagnostics;

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

// Your code for waiting and printing results goes here.

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
    // Implement asynchronous processing.
    throw new NotImplementedException();
}