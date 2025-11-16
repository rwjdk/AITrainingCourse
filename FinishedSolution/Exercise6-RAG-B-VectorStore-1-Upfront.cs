using Azure.AI.OpenAI;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel.Connectors.InMemory;
using OpenAI;

namespace FinishedSolution;

public class Exercise6RagVectorStoreUpfront
{
    public static async Task Run(InMemoryCollection<Guid, KnowledgeBaseVectorRecord> collection, AzureOpenAIClient client, string model)
    {
        ChatClientAgent agent = client.GetChatClient(model).CreateAIAgent();

        while (true)
        {
            Console.Write("> ");
            string input = Console.ReadLine() ?? string.Empty;
            if (input.Equals("q", StringComparison.CurrentCultureIgnoreCase))
            {
                break;
            }

            List<ChatMessage> ragPreloadChatMessages =
            [
                new(ChatRole.Assistant, "Here are the most relevant knowledge entries")
            ];
            await foreach (VectorSearchResult<KnowledgeBaseVectorRecord> searchResult in collection.SearchAsync(input, top: 3))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                KnowledgeBaseVectorRecord searchResultRecord = searchResult.Record;
                Console.WriteLine($"- RAG Search Result: Q: {searchResultRecord.Question} - A: {searchResultRecord.Answer} [Score: {searchResult.Score}]");
                Console.ResetColor();
                ragPreloadChatMessages.Add(new ChatMessage(ChatRole.User, $"Q: {searchResultRecord.Question} - A: {searchResultRecord.Answer}"));
            }

            ChatMessage message = new(ChatRole.User, input);
            AgentRunResponse response = await agent.RunAsync(ragPreloadChatMessages.Union([message]));
            Console.WriteLine(response);

            Console.WriteLine("---");
            if (response.Usage != null)
            {
                Console.WriteLine($"Input: {response.Usage.InputTokenCount} - Output: {response.Usage.OutputTokenCount}");
            }
        }
    }
}