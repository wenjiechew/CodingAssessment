namespace CodingAssignmentLib.Abstractions;

public interface IReaderFactory
{
    IContentParser CreateReader(string fileName);
}