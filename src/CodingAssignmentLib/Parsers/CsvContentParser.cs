using CodingAssignmentLib.Abstractions;

namespace CodingAssignmentLib.Parsers;

public class CsvContentParser : IContentParser
{
    public Task<IEnumerable<Data>> Parse(string content)
    {
        var list = content.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Select(line =>
        {
            var items = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
            return new Data(items[0], items[1]);
        });
        
        return Task.FromResult(list);
    }
}