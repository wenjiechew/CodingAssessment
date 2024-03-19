// See https://aka.ms/new-console-template for more information

using System.IO.Abstractions;
using CodingAssignmentLib;
using CodingAssignmentLib.Abstractions;
using CodingAssignmentLib.Parsers;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Coding Assignment!");
var services = new ServiceCollection();
ConfigureServices(services);
var serviceProvider = services.BuildServiceProvider();

var factory = serviceProvider.GetRequiredService<IReaderFactory>();
var fileUtility = serviceProvider.GetRequiredService<IFileUtility>();

do
{
    Console.WriteLine("\n---------------------------------------\n");
    Console.WriteLine("Choose an option from the following list:");
    Console.WriteLine("\t1 - Display");
    Console.WriteLine("\t2 - Search");
    Console.WriteLine("\t3 - Exit");

    switch (Console.ReadLine())
    {
        case "1":
            Display();
            break;
        case "2":
            Search();
            break;
        case "3":
            return;
        default:
            return;
    }
} while (true);


void Display()
{
    Console.WriteLine("Enter the name of the file to display its content:");

    var fileName = Console.ReadLine()!;
    //var fileUtility = new FileUtility(new FileSystem());
    var dataList = Enumerable.Empty<Data>();
    
    var reader = factory.CreateReader(fileUtility.GetExtension(fileName));
    dataList = reader.Parse(fileUtility.GetContent(fileName));
    //if (fileUtility.GetExtension(fileName) == ".csv")
    //{
    //    dataList = new CsvContentParser().Parse(fileUtility.GetContent(fileName));
    //}

    Console.WriteLine("Data:");

    foreach (var data in dataList)
    {
        Console.WriteLine($"Key:{data.Key} Value:{data.Value}");
    }
}

void Search()
{
    var allFiles = fileUtility.GetAllFiles();
    Console.WriteLine("Enter the key to search.");
    var searchKey = Console.ReadLine()!;

    foreach (var file in allFiles)
    {
        var reader = factory.CreateReader(fileUtility.GetExtension(file));
        var list = reader.Parse(fileUtility.GetContent(file));
        
        var searchedCollection = list
            .Where(e => string.Equals(e.Key, searchKey, StringComparison.CurrentCultureIgnoreCase));
        
        foreach (var eachData in searchedCollection)
        {
            Console.WriteLine($"Key:{eachData.Key} Value:{eachData.Value} FileName: {file}");
        }
    }
    




        
    
}

static void ConfigureServices (IServiceCollection services )
{
    services.AddSingleton<IReaderFactory, ReaderFactory>();
    services.AddSingleton<IFileUtility, FileUtility>();
    services.AddSingleton<IFileSystem, FileSystem>();
    
    services.AddTransient<CsvContentParser>();
    services.AddTransient<JsonContentParser>();
    services.AddTransient<XmlContentParser>();
}
