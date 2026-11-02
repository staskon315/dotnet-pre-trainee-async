# dotnet-pre-trainee-async

A .NET console application demonstrating the difference between synchronous blocking (`Thread.Sleep`) and asynchronous non-blocking (`await Task.Delay`) execution. It simulates the processing of three files with varying delays, compares the total execution time, and showcases `Task.WhenAll`, `Task.WhenAny`, and asynchronous error handling.

## Results

```
Synchronous processing:  9.01 seconds
Asynchronous processing: 3.01 seconds
```

In synchronous mode, files are processed sequentially (3 + 3 + 3 ≈ 9 seconds). In asynchronous mode, all three tasks start simultaneously; the wait periods overlap, resulting in a total time of ≈ 3 seconds (determined by the longest-running operation). In `WhenAny` mode, results are output as each task completes: File 2 → File 3 → File 1.

## Blocking vs. Non-blocking Waiting

**Blocking wait (`Thread.Sleep`)** — the current thread is entirely occupied by the wait and cannot perform any other work. While it is "sleeping," it remains idle. This is why three synchronous operations, each lasting 3 seconds and executed one after another, take a total of ≈ 9 seconds: the second operation cannot begin until the first has finished waiting.

**Non-blocking wait (`await Task.Delay`)** — the thread is not held up by the wait. `Task.Delay` returns a `Task` that will complete later based on a timer, while control is immediately returned to the calling code.

## Answers to Questions

**1. Why did the synchronous version take longer?**
The operations were performed sequentially: each subsequent operation began only after the previous one had finished. 3 × 3 seconds = 9 seconds.

**2. **Why can asynchronous wait periods overlap?**
`await Task.Delay` does not block the thread; instead, it releases it. All tasks start nearly simultaneously, their wait periods occur in parallel, and the total time taken is equal to the duration of the longest operation.

**3. Does `async` automatically create a new thread?**
No, `async` does not automatically create threads.

**4. What is the difference between `Thread.Sleep` and `Task.Delay`?**
`Thread.Sleep` blocks the current thread for a specified duration. `Task.Delay` returns a `Task` that completes later without blocking the thread—the thread is freed up to perform other work.

**5. Why is this important in ASP.NET Core?**
The thread pool is limited. Blocking a thread using `Thread.Sleep` or synchronous I/O operations depletes the pool and reduces throughput. Asynchronous waiting releases the thread during I/O operations, allowing the application to handle more requests.

**6. What is the difference between `Task.WhenAll` and `Task.WhenAny`?**
`WhenAll` waits for all tasks to complete. `WhenAny` waits for the first task to complete and returns that task.

**7. Does using asynchronous code always make it faster?**
No. Asynchrony does not speed up the operation itself; rather, it prevents the thread from being blocked while waiting. For sequential, dependent operations, there is no time gain—in fact, there is a slight overhead. Any reduction in total execution time occurs only because independent wait periods are allowed to overlap.

---

## How to Copy and Run



### 1. Clone the repository

```bash
https://github.com/staskon315/dotnet-pre-trainee-async.git

```



### 2. Navigate to the project folder

```bash
cd dotnet-pre-trainee-async
```


### 3. Run the application

```bash
dotnet run
```
