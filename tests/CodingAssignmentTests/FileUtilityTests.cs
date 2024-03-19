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
        // Arrange
        var fileUtilityInstance = new FileUtility(new FileSystem());

        // Act
        var fileExtension = fileUtilityInstance.GetExtension(fileName);
        
        // Assert
        Assert.Equal(expected, fileExtension);
    }
    
    [Fact]
    public void GetFileContents_WithFile_ReturnsExpectedFileContents()
    {
        // Arrange
        var fileUtilityInstance = new FileUtility(new FileSystem());
        var fileName = "TestFiles/test.csv";

        // Act
        var fileContent = fileUtilityInstance.GetContent(fileName);
        
        // Assert
        Assert.NotEmpty(fileContent);
    }
    
    [Fact]
    public void GetAllFile_WithFile_ReturnsAllFilePathFromDirectory()
    {
        // Arrange
        var fileUtilityInstance = new FileUtility(new FileSystem());
        var directory = "TestFiles";

        // Act
        var files = fileUtilityInstance.GetAllFiles(directory);
        
        // Assert
        Assert.NotEmpty(files);
    }
}