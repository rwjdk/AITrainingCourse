using Azure.AI.OpenAI;
using Microsoft.Agents.AI;
using OpenAI;

namespace FinishedSolution;

public static class Exercise5StructuredOutput
{
    public static async Task Run(AzureOpenAIClient client, string model)
    {
        /* Exercise 5: Structured Output
         1. Get the Agent to output what IMDB's Top 3 movies are
         2. Use instructions to guide the AI to follow a specific format of the output (Title, Director, Year of Release and Score)
         3. Now do it as structured output instead to have better control what data comes back
        */

        Console.WriteLine("- AGENT FORMAT (VARIES) -");
        ChatClientAgent agent = client.GetChatClient(model).CreateAIAgent();
        AgentRunResponse responseWhereAIDecideFormat = await agent.RunAsync("List the top 3 best movies according to IMDB");
        Console.WriteLine(responseWhereAIDecideFormat);

        Console.WriteLine();
        Console.WriteLine("---");
        Console.WriteLine();

        Console.WriteLine("- AGENT GUIDED FORMAT -");
        AgentRunResponse responseWhereYouGuide = await agent.RunAsync("List the top 3 best movies according to IMDB. It needs to be in format '- <Title> (<Director>) [<YearOfRelease>]: <ImdbScore>'");
        Console.WriteLine(responseWhereYouGuide);

        Console.WriteLine();
        Console.WriteLine("---");
        Console.WriteLine();

        Console.WriteLine("- STRUCTURED OUTPUT -");
        ChatClientAgentRunResponse<List<Movie>> responseWithStructureOutput = await agent.RunAsync<List<Movie>>("List the top 3 best movies according to IMDB");
        foreach (Movie movie in responseWithStructureOutput.Result)
        {
            Console.WriteLine($"- {movie.Title} ({movie.Director}) [{movie.YearOfRelease}]: {movie.ImdbScore}");
        }
    }

    public record Movie(string Title, string Director, int YearOfRelease, decimal ImdbScore);
}