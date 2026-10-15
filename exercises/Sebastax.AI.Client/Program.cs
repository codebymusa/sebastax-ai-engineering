
using OllamaSharp;
using System.Diagnostics;

const string endpoint = "http://localhost:11434";
const string model = "llama3.2:3b";

// Create a client for the locally running model.
var client = new OllamaApiClient(endpoint, model);

Console.WriteLine("SEBASTAX AI Client");
Console.WriteLine($"Model: {model}");
Console.WriteLine("Type 'exit' to quit.");

while (true)
{
    Console.Write("\nEnter your prompt: ");
    string? prompt = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(prompt))
    {
        Console.WriteLine("Please enter a prompt.");
        continue;
    }

    if (prompt.Equals("exit", StringComparison.OrdinalIgnoreCase))
        break;

    var stopwatch = Stopwatch.StartNew();

    try
    {
        Console.WriteLine("\nAI Response:\n");

        await foreach (var chunk in client.GenerateAsync(prompt))
        {
            Console.Write(chunk?.Response);
        }

        Console.WriteLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Request failed: {ex.Message}");
    }
    finally
    {
        stopwatch.Stop();

        Console.WriteLine(
            $"\nRequest duration: {stopwatch.Elapsed.TotalSeconds:F2} seconds");
    }
}
