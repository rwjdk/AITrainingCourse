using Azure.AI.OpenAI;
using Microsoft.Agents.AI;
using OpenAI;

namespace FinishedSolution;

public static class Exercise2HelloWorld
{
    public static async Task Run(AzureOpenAIClient client, string model)
    {
        /* Exercise 2: Hello World
         1. From the client get a ChatClient with the gpt-4.1-model and convert it into an AIAgent (ChatClientAgent) using CreateAIAgent()
         2. Ask the agent a question using RunAsync(...)
         3. Print the answer in the console
         4. (Bonus) Make it a streaming response instead
         5. (Bonus) Give you Agent rules and personality using the optional "instructions" parameter you can give to CreateAIAgent(...)
         6. (Bonus) Find out how many tokens you use in input and output
         */

        ChatClientAgent agent = client.GetChatClient(model).CreateAIAgent();
        AgentRunResponse response = await agent.RunAsync("What is the capital of France?");
        Console.WriteLine(response);

        //Step 4 (Bonus)
        await foreach (AgentRunResponseUpdate update in agent.RunStreamingAsync("What is the capital of France?"))
        {
            Console.Write(update);
        }

        Console.WriteLine();

        //Step 5 (Bonus)
        ChatClientAgent agentPersonal = client.GetChatClient(model).CreateAIAgent(instructions: "Speak like a pirate");
        AgentRunResponse responsePersonal = await agentPersonal.RunAsync("What is the capital of France?");
        Console.WriteLine(responsePersonal);

        //Step 6 (Bonus)
        if (responsePersonal.Usage != null)
        {
            Console.WriteLine($"Input: {responsePersonal.Usage.InputTokenCount} - Output: {responsePersonal.Usage.OutputTokenCount}");
        }
    }
}