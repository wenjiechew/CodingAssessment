using System.IO.Abstractions;
using CodingAssignmentLib;
using Xunit;
using Assert = Xunit.Assert;

namespace CodingAssignmentTests;

public class FileUtilityTests
{
    [Xunit.Theory]
    [InlineData(".json", "data.json")]
    [InlineData(".xml", "data.xml")]
    [InlineData(".csv", "data.csv")]
    public void GetFileExtension_WithFile_ReturnsExpectedFileExtensions(string expected, string fileName)
    {
        var fileUtilityInstance = new FileUtility(new FileSystem());

        var fileExtension = fileUtilityInstance.GetExtension(fileName);
        
        Assert.Equal(expected, fileExtension);
    }
    
    [Fact]
    public void GetFileContents_WithFile_ReturnsExpectedFileContents()
    {
        var fileUtilityInstance = new FileUtility(new FileSystem());
        var fileName = "TestFiles/test.csv";

        var fileContent = fileUtilityInstance.GetContent(fileName);
        
        Assert.NotEmpty(fileContent);
    }
    
    [Fact]
    public void GetAllFile_WithFile_ReturnsAllFilePathFromDirectory()
    {
        var fileUtilityInstance = new FileUtility(new FileSystem());
        var directory = "TestFiles";

        var files = fileUtilityInstance.GetAllFiles(directory);
        
        Assert.NotEmpty(files);
    }
}