using CodingAssignmentLib.Abstractions;
using CodingAssignmentLib.Parsers;
using Xunit;
using Assert = Xunit.Assert;


namespace CodingAssignmentTests;

public class ParserTests
{
    [Fact]
    public async Task Parse_ReturnsExpectedData_FromJsonString()
    {
        // Arrange
        var parser = new JsonContentParser();
        var jsonContent = "[{\"Key\": \"aaa\", \"Value\": \"aaa\"}, {\"Key\": \"bbbb\", \"Value\": \"bbbb\"}]";

        // Act
        var result = await parser.Parse(jsonContent);

        // Assert
        Assert.Collection(result,
            item => Assert.Equal(new Data("aaa", "aaa"), item),
            item => Assert.Equal(new Data("bbbb", "bbbb"), item));
    }
    
    [Fact]
    public async Task Parse_ReturnsExpectedData_FromXmlString()
    {
        // Arrange
        var parser = new XmlContentParser();
        var xmlContent = "<Datas><Data><Key>someKey</Key><Value>someValue</Value></Data><Data><Key>someKey1</Key><Value>someValue1</Value></Data></Datas>";

        // Act
        var result = await parser.Parse(xmlContent);

        // Assert
        Assert.Collection(result,
            item => 
                Assert.Equal(new Data("someKey", "someValue"), item),
            item => Assert.Equal(new Data("someKey1", "someValue1"), item));
    }
}