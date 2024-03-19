using System.IO.Abstractions;
using CodingAssignmentLib.Abstractions;

namespace CodingAssignmentLib;

public class FileUtility : IFileUtility
{
    private readonly IFileSystem _fileSystem;

    public FileUtility(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }
    
    public string GetExtension(string fileName)
    {
        return _fileSystem.FileInfo.New(fileName).Extension;
    }

    public string GetContent(string fileName)
    {
        return _fileSystem.File.ReadAllText(fileName);
    }

    public IEnumerable<string> GetAllFiles(string directory)
    {
        // *.* -> the first * represents wildcard of fileName, second * represents the wildcard of fileExtensions
        var files = _fileSystem.Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories)
            .Where(IsSupportedFileType);

        return files;
    }

    private static bool IsSupportedFileType(string filePath)
    {
        return filePath.EndsWith(".csv", StringComparison.OrdinalIgnoreCase) ||
               filePath.EndsWith(".json", StringComparison.OrdinalIgnoreCase) ||
               filePath.EndsWith(".xml", StringComparison.OrdinalIgnoreCase);
    }

}