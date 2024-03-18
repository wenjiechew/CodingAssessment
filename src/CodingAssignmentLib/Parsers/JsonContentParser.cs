using CodingAssignmentLib.Abstractions;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace CodingAssignmentLib.Parsers;

public class JsonContentParser : IContentParser
{
    public IEnumerable<Data> Parse(string content)
    {
        var list = new List<Data>();
        var jsonArray = JsonSerializer.Deserialize<JsonArray>(content);
        foreach (var node in jsonArray)
        {
            list.Add(new Data(node["Key"].ToString(), node["Value"].ToString()));
        }
        return list.AsEnumerable();
    }
}