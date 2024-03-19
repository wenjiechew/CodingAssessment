using System.Xml.Linq;
using System.Xml.Serialization;
using CodingAssignmentLib.Abstractions;

namespace CodingAssignmentLib.Parsers;

public class XmlContentParser : IContentParser
{
    public IEnumerable<Data> Parse(string content)
    {
        var list = new List<Data>();
        
        var doc = XDocument.Parse(content);
        //foreach (var element in doc.Root!.DescendantNodes("Data"))
        foreach (var element in doc.Descendants("Data"))
        {
            var key = element.Element("Key")?.Value;
            var Value = element.Element("Value")?.Value;
            
            list.Add(new Data(key, Value));
        }

        return list.AsEnumerable();
    }
}