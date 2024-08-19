
using IndigoSoftTest;
using System.Diagnostics;

var nativeRate = new NativeRate();


var nativeRateCollections= new List<NativeRate>();


for (int i = 0; i < 1000; i++)
{
    var random= new Random();
    var randomnNext = random.Next(1,100);
    nativeRateCollections.Add(new NativeRate()
    {
        Time = DateTime.Now.AddDays(randomnNext),
        Ask = randomnNext,
        Bid = randomnNext + 5,
        Symbol = "Syymbol" + i.ToString()

    });

   
  
}
var rateStorge = new RatesStorage();
List<Task> tasks = new List<Task>();
var watchOne = Stopwatch.StartNew();
foreach (var item in nativeRateCollections)
{
    tasks.Add(rateStorge.UpdateRateAsync(item));
    tasks.Add(rateStorge.GetRateAsync(item.Symbol));

}
await  Task.WhenAll(tasks);
watchOne.Stop();

Console.WriteLine(
    $"The Execution time of the program is {watchOne.ElapsedMilliseconds}ms when two method");




var watchTwo = Stopwatch.StartNew();
foreach (var item in nativeRateCollections)
{
    tasks.Add(rateStorge.ShowUpdatedRateAsync(item));
    

}
await Task.WhenAll(tasks);
watchTwo.Stop();

Console.WriteLine(
    $"The Execution time of the program is {watchTwo.ElapsedMilliseconds}ms  when one method");



//// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
