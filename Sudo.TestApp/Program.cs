using System.Diagnostics;
using OpenCvSharp;
using Sudo.Core.Models;

var file = @"D:\trainingTest\sudo1.jpg";
var traningDataFolder = @"D:\training";

Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();
Sudo.OCR.OCRService ocr = new Sudo.OCR.OCRService(file);
Console.WriteLine("Loading training images");
var images = ocr.ReadTrainingImages(traningDataFolder, "*.jpg");
Console.WriteLine("Trainint...");
var kNearest = ocr.TrainData(images);
var model = @"D:\CSharpGames\Sudo\SudoDemo\KNearestModel\number_ocr";
if(File.Exists(model))
    File.Delete(model);
kNearest.Save(model);

//var kNearest = OpenCvSharp.ML.KNearest.Load(@"D:\CSharpGames\Sudo\SudoDemo\KNearestModel\number_ocr");

Console.WriteLine("OCR Start...");
var ocrNumbers = ocr.DoOCR(kNearest,out Mat[] results);
for (int row = 0; row < 9; row++)
{
    for (int col = 0; col < 9; col++)
    {
        Console.Write(ocrNumbers[(row * 9) + col]);
    }
    Console.WriteLine();
}

Console.WriteLine("Try to resolve sudo.");
Sudo.Core.Sudoku sudoku = new Sudo.Core.Sudoku(ocrNumbers);
var isResolved = sudoku.TryResolve(out SudoNode[,] result, ocrNumbers);
stopwatch.Stop();
if(isResolved)
{
    Console.WriteLine("Resolved:");
    for (int row = 0; row < 9; row++)
    {
        for (int col = 0; col < 9; col++)
        {
            Console.Write(result[row,col].Value);
        }
        Console.WriteLine();
    }
}
else
{
    Console.WriteLine("无解");
}
Console.WriteLine($"Cost: {stopwatch.ElapsedMilliseconds} ms");