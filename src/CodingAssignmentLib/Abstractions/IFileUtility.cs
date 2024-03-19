using System.Collections;

namespace CodingAssignmentLib.Abstractions;

public interface IFileUtility
{
    string GetExtension(string fileName);
    string GetContent(string fileName);

    IEnumerable<string> GetAllFiles(string directory = "data/");
}