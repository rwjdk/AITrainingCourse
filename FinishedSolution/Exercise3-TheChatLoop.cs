using Azure.AI.OpenAI;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using OpenAI;

namespace FinishedSolution;

public static class Exercise3TheChatLoop
{
    public static async Task Run(AzureOpenAIClient client, string model)
    {
        /* Exercise 3: The Chat Loop
         1. Let's make a chat loop so we can input questions
         2. Let's add a AgentThread so it is a continuous conversation
         */

        ChatClientAgent agent = client.GetChatClient(model).CreateAIAgent(instructions: "Speak like a pirate");

        AgentThread thread = agent.GetNewThread();

        while (true)
        {
            Console.Write("> ");
            string input = Console.ReadLine() ?? string.Empty;
            if (input.Equals("q", StringComparison.CurrentCultureIgnoreCase))
            {
                break;
            }

            ChatMessage message = new(ChatRole.User, input);
            AgentRunResponse response = await agent.RunAsync(message, thread);
            Console.WriteLine(response);

            Console.WriteLine("---");
        }

        Console.WriteLine("What is in the thread");
        InMemoryChatMessageStore messageStore = thread.GetService<InMemoryChatMessageStore>()!;
        IEnumerable<ChatMessage> chatMessages = await messageStore.GetMessagesAsync();
        foreach (ChatMessage message in chatMessages)
        {
            Console.WriteLine($"- [{message.Role}] {message.Text}");
        }
    }
}