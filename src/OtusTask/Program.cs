using System.Diagnostics;
using OtusTask;

const string filesFolderName = "/Files";

var stopwatch = Stopwatch.StartNew();

var currentDir = FilesHandler.GetProjectDirectory();
var fullPath = Path.GetFullPath(currentDir + filesFolderName);

var totalSpaces = await FilesHandler.StartAsync(fullPath);
Console.WriteLine($"Во всех файлах в папке пробелов = {totalSpaces}");

stopwatch.Stop();
Console.WriteLine($"Время выполнения кода - {stopwatch.ElapsedMilliseconds} мс.");
