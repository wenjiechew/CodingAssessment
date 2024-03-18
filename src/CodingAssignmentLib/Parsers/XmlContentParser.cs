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
        foreach (var element in doc.Root!.Elements())
        {
            list.Add(new Data(element.Name.LocalName, element.Value));
        }

        return list.AsEnumerable();
    }
}