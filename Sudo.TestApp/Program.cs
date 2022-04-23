using System.Diagnostics;
using System.Reflection.Metadata;
using Game.Service.Sudo.Common;

Console.WriteLine("Hi");

Game.Service.Sudo.Sudoku sudoku = new Game.Service.Sudo.Sudoku();
var sudoModel = sudoku.SudoModel;
Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();
Game.Service.Sudo.Resolver resolver = new Game.Service.Sudo.Resolver(sudoModel,true);
var isResolved = resolver.Resolve(out int deep);
Console.WriteLine($"IsResolved:{isResolved} , Resolve count:{resolver.Resolves.Count} , Deep:{deep}");
stopwatch.Stop();
Console.WriteLine($"Cost:{stopwatch.ElapsedMilliseconds}ms");
foreach (var item in resolver.Resolves)
{
    Console.WriteLine("-----Resolve---->");
    for (int row = 0; row < 9; row++)
    {
        Console.WriteLine(string.Join(",", item.SliceRow(row).Select(a=>a.Value)));
    }
}