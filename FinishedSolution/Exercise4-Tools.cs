using Azure.AI.OpenAI;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using OpenAI;

namespace FinishedSolution;

public static class Exercise4Tools
{
    public static async Task Run(AzureOpenAIClient client, string model)
    {
        /* Exercise 3: Tool Calling
         1. Give your agent an Information Tool (example a tool that based on name can give info about a person given a name as input)
         2. Make an Action Tool (example a tool that change the color of the text in the Console)
        */

        AIFunction infoTool = AIFunctionFactory.Create(GetPersonInfoTool);
        ChatClientAgent infoAgent = client.GetChatClient(model).CreateAIAgent(tools: [infoTool]);
        AgentRunResponse infoResponse = await infoAgent.RunAsync("What is Rasmus' favorite color?");
        Console.WriteLine(infoResponse);

        AIFunction actionTool = AIFunctionFactory.Create(ChangeConsoleColor);
        ChatClientAgent colorAgent = client.GetChatClient(model).CreateAIAgent(tools: [actionTool, infoTool]);

        AgentRunResponse colorResponse = await colorAgent.RunAsync("Change the color of the Console to blue");
        Console.WriteLine(colorResponse);

        colorResponse = await colorAgent.RunAsync("Change the color of the Console the main color of the canadian flag");
        Console.WriteLine(colorResponse);

        colorResponse = await colorAgent.RunAsync("Change the color of the Console to Troels' Favorite color");
        Console.WriteLine(colorResponse);
    }

    public record PersonInfo(string Name, string FavoriteColor, string City);

    //Information Tool
    public static PersonInfo? GetPersonInfoTool(string name)
    {
        switch (name.ToUpperInvariant())
        {
            case "RASMUS":
                return new PersonInfo("Rasmus", "Blue", "Åbyhøj");
            case "TROELS":
                return new PersonInfo("Troels", "Yellow", "Århus");
            case "SØREN":
                return new PersonInfo("Søren", "Green", "Silkeborg");
            default:
                return null;
        }
    }

    //Action Tool
    public static void ChangeConsoleColor(ConsoleColor color)
    {
        Console.ForegroundColor = color;
    }
}