using CodingAssignmentLib;
using CodingAssignmentLib.Abstractions;
using CodingAssignmentLib.Parsers;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;
using Assert = Xunit.Assert;


namespace CodingAssignmentTests;

public class ReaderFactoryTests
{
    private readonly Mock<IFileUtility > _fileUtilityMock;
    private readonly Mock<IServiceProvider> _serviceProviderMock;
    
    public ReaderFactoryTests()
    {
        _fileUtilityMock = new Mock<IFileUtility>();
        _serviceProviderMock = new Mock<IServiceProvider>();
    }

    [Fact]
    public void CreateReader_WithUnsupportedFileExtension_ThrowsNotSupportedException()
    {
        // Arrange
        _fileUtilityMock.Setup(fu => fu.GetExtension(It.IsAny<string>())).Returns(".txt");
        var readerFactory = new ReaderFactory(_fileUtilityMock.Object, _serviceProviderMock.Object);

        // Act & Assert
        Assert.Throws<NotSupportedException>(() => readerFactory.CreateReader("test.txt"));
    }

    [Fact]
    public void CreateReader_WithJsonFile_ReturnsJsonReader()
    {
        // Arrange
        _fileUtilityMock.Setup(fu => fu.GetExtension(It.IsAny<string>())).Returns(".json");
        _serviceProviderMock.Setup(sp => sp.GetService(typeof(JsonContentParser))).Returns(new JsonContentParser());

        var readerFactory = new ReaderFactory(_fileUtilityMock.Object, _serviceProviderMock.Object);

        // Act
        var reader = readerFactory.CreateReader("test.json");

        // Assert
        Assert.IsType<JsonContentParser>(reader);
    }

    [Fact]
    public void CreateReader_WithXmlFile_ReturnsXmlReader()
    {
        // Arrange
        _fileUtilityMock.Setup(fu => fu.GetExtension(It.IsAny<string>())).Returns(".xml");
        _serviceProviderMock.Setup(sp => sp.GetService(typeof(XmlContentParser))).Returns(new XmlContentParser());

        var readerFactory = new ReaderFactory(_fileUtilityMock.Object, _serviceProviderMock.Object);

        // Act
        var reader = readerFactory.CreateReader("test.xml");

        // Assert
        Assert.IsType<XmlContentParser>(reader);
    }
    
    [Fact]
    public void CreateReader_WithCsvFile_ReturnsCsvReader()
    {
        // Arrange
        _fileUtilityMock.Setup(fu => fu.GetExtension(It.IsAny<string>())).Returns(".csv");
        _serviceProviderMock.Setup(sp => sp.GetService(typeof(CsvContentParser))).Returns(new CsvContentParser());


        var readerFactory = new ReaderFactory(_fileUtilityMock.Object, _serviceProviderMock.Object);

        // Act
        var reader = readerFactory.CreateReader("test.csv");

        // Assert
        Assert.IsType<CsvContentParser>(reader);
    }
}
    
