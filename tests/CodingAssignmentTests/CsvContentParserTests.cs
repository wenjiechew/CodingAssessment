using CodingAssignmentLib.Abstractions;
using CodingAssignmentLib.Parsers;

namespace CodingAssignmentTests;

public class CsvContentParserTests
{
    private CsvContentParser _sut = null!;
    
    [SetUp]
    public void Setup()
    {
        _sut = new CsvContentParser();
    }

    [Test]
    public async Task Parse_ReturnsData()
    {
        var content = "a,b" + Environment.NewLine + "c,d" + Environment.NewLine;
        var dataList = await _sut.Parse(content);
        CollectionAssert.AreEqual(new List<Data>
        {
            new("a", "b"),
            new("c", "d")
        }, dataList);
    }
}