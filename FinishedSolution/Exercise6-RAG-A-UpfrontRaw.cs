using Azure.AI.OpenAI;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using OpenAI;

namespace FinishedSolution;

public class Exercise6RagUpfrontRaw
{
    public static async Task Run(List<KnowledgeBaseEntry> knowledgeBase, AzureOpenAIClient client, string model)
    {
        Console.WriteLine("- DATA UP FRONT -");
        ChatClientAgent agent = client.GetChatClient(model).CreateAIAgent();
        List<ChatMessage> upfrontMessages =
        [
            new(ChatRole.User, "Here is the full Company Knowledge base")
        ];
        upfrontMessages.AddRange(knowledgeBase.Select(x => new ChatMessage(ChatRole.User, $"Q: {x.Question} - A: {x.Answer}")));


        while (true)
        {
            Console.Write("> ");
            string input = Console.ReadLine() ?? string.Empty;
            if (input.Equals("q", StringComparison.CurrentCultureIgnoreCase))
            {
                break;
            }

            ChatMessage message = new(ChatRole.User, input);
            AgentRunResponse response = await agent.RunAsync(upfrontMessages.Union([message]));
            Console.WriteLine(response);

            Console.WriteLine("---");
            if (response.Usage != null)
            {
                Console.WriteLine($"Input: {response.Usage.InputTokenCount} - Output: {response.Usage.OutputTokenCount}");
            }
        }
    }
}