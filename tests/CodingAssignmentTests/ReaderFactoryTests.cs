using CodingAssignmentLib;
using CodingAssignmentLib.Abstractions;
using CodingAssignmentLib.Parsers;
using Moq;
using Xunit;
using Assert = Xunit.Assert;


namespace CodingAssignmentTests;

public class ReaderFactoryTests
{
    private readonly Mock<IServiceProvider> _serviceProviderMock;
    
    public ReaderFactoryTests()
    {
        _serviceProviderMock = new Mock<IServiceProvider>();
    }

    [Fact]
    public void CreateReader_WithUnsupportedFileExtension_ThrowsNotSupportedException()
    {
        // Arrange
        var readerFactory = new ReaderFactory(_serviceProviderMock.Object);

        // Act & Assert
        Assert.Throws<NotSupportedException>(() => readerFactory.CreateReader("test.txt"));
    }

    [Fact]
    public void CreateReader_WithJsonFile_ReturnsJsonReader()
    {
        // Arrange
        _serviceProviderMock.Setup(sp => sp.GetService(typeof(JsonContentParser))).Returns(new JsonContentParser());

        var readerFactory = new ReaderFactory(_serviceProviderMock.Object);

        // Act
        var reader = readerFactory.CreateReader(".json");

        // Assert
        Assert.IsType<JsonContentParser>(reader);
    }

    [Fact]
    public void CreateReader_WithXmlFile_ReturnsXmlReader()
    {
        // Arrange
        _serviceProviderMock.Setup(sp => sp.GetService(typeof(XmlContentParser))).Returns(new XmlContentParser());

        var readerFactory = new ReaderFactory(_serviceProviderMock.Object);

        // Act
        var reader = readerFactory.CreateReader(".xml");

        // Assert
        Assert.IsType<XmlContentParser>(reader);
    }
    
    [Fact]
    public void CreateReader_WithCsvFile_ReturnsCsvReader()
    {
        // Arrange
        _serviceProviderMock.Setup(sp => sp.GetService(typeof(CsvContentParser))).Returns(new CsvContentParser());


        var readerFactory = new ReaderFactory(_serviceProviderMock.Object);

        // Act
        var reader = readerFactory.CreateReader(".csv");

        // Assert
        Assert.IsType<CsvContentParser>(reader);
    }
}
    
