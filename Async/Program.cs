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

try
{
    await Task.WhenAll(tasks);
}
catch
{
    Console.WriteLine("Processing File 2 failed: Unable to process data.");
}
foreach (var task in tasks)
{
    if (task.IsCompletedSuccessfully)
        Console.WriteLine(task.Result);

}


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
    var completedTask = await Task.WhenAny(tasks2);
    string completed;
    try { completed = await completedTask; }
    catch
    {
        completed = "Processing File 2 failed: Unable to process data.";
    }

    Console.WriteLine(completed);
    tasks2.Remove(completedTask);

}

asyncWatch.Stop();

Console.WriteLine(
    $"Asynchronous time: {asyncWatch.Elapsed.TotalSeconds:F2} seconds \n");

Console.WriteLine($"Results:\n  Synchronous processing: {syncWatch.Elapsed.TotalSeconds:F2} seconds\nAsynchronous processing: {asyncWatch.Elapsed.TotalSeconds:F2} seconds");



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

    if (dataName != "File 2")
        await Task.Delay(delayMilliseconds);
    else throw new IOException();
    asyncWatchFile.Stop();
    return $"ProProcessing '{dataName}' completed in {asyncWatchFile.Elapsed.TotalSeconds:F2} seconds.";
}