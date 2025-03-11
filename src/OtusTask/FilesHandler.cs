using System.Runtime.CompilerServices;

namespace OtusTask;

public static class FilesHandler
{
    public static async Task<int> StartAsync(string folderPath)
    {
        if (IsFolderValid(folderPath) == false) return 0;
        
        var files = Directory.GetFiles(folderPath);
        var tasks = files.Select(CountSpaceFromFile).ToArray();
        
        var result = await Task.WhenAll(tasks);
        return result.Sum();
    }

    private static bool IsFolderValid(string folderPath)
    {
        return Directory.Exists(folderPath) && Directory.GetFiles(folderPath).Length > 0;
    }
    
    private static Task<int> CountSpaceFromFile(string filePath)
    {
        var spacesCount = 0;
        //Читаем побитово, чтобы не загружать в память содержимое всего файла
        using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        
        int currentByte;
        while ((currentByte = fileStream.ReadByte()) != -1)
        {
            if (currentByte == ' ')
            {
                spacesCount++;
            }
        }

        return Task.FromResult(spacesCount);
    }


    /// <summary>
    /// Текущая директория, откуда запущен Main
    /// </summary>
    /// <param name="callerFilePath"></param>
    /// <returns></returns>
    public static string GetProjectDirectory([CallerFilePath] string callerFilePath = "")
    {
        return Path.GetDirectoryName(callerFilePath)!;
    }
}