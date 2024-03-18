﻿// See https://aka.ms/new-console-template for more information

using System.IO.Abstractions;
using CodingAssignmentLib;
using CodingAssignmentLib.Abstractions;
using CodingAssignmentLib.Parsers;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Coding Assignment!");
var services = new ServiceCollection();
ConfigureServices(services);
var serviceProvider = services.BuildServiceProvider();


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
    
    var factory = serviceProvider.GetRequiredService<IReaderFactory>();
    var fileUtility = serviceProvider.GetRequiredService<IFileUtility>();
    
    var reader = factory.CreateReader(fileName);
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
    Console.WriteLine("Enter the key to search.");
}

static void ConfigureServices (IServiceCollection services )
{
    services.AddSingleton<IReaderFactory, ReaderFactory>();
    services.AddSingleton<IFileUtility, FileUtility>();
    services.AddSingleton<IFileSystem, FileSystem>();
    
    
    // services.AddSingleton<Func<string, IContentParser>>(serviceProvider => key =>
    // {
    //     switch (key)
    //     {
    //         case ".csv":
    //             return serviceProvider.GetRequiredService<CsvContentParser>();
    //         case ".json":
    //             return serviceProvider.GetRequiredService<JsonContentParser>();
    //         default:
    //             throw new NotSupportedException($"File extension {key} is not supported");
    //     }
    // });
    services.AddTransient<CsvContentParser>();
    services.AddTransient<JsonContentParser>();
}
