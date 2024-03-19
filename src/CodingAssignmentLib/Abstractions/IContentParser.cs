namespace CodingAssignmentLib.Abstractions;

public interface IContentParser
{
    Task<IEnumerable<Data>> Parse(string content);
}