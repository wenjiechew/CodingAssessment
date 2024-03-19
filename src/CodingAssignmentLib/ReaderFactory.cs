using CodingAssignmentLib.Abstractions;
using CodingAssignmentLib.Parsers;
using Microsoft.Extensions.DependencyInjection;

namespace CodingAssignmentLib;

public class ReaderFactory : IReaderFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ReaderFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IContentParser CreateReader(string fileExtension)
    {
        switch (fileExtension)
        {
            case ".csv":
                return _serviceProvider.GetRequiredService<CsvContentParser>();
            case ".json":
                return _serviceProvider.GetRequiredService<JsonContentParser>();
            case ".xml":
                return _serviceProvider.GetRequiredService<XmlContentParser>();
            default:
                throw new NotSupportedException($"File extension {fileExtension} is not supported");
        }
    }
}