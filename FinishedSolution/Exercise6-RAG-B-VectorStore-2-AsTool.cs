using Azure.AI.OpenAI;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel.Connectors.InMemory;
using OpenAI;

namespace FinishedSolution;

public class Exercise6RagVectorStoreAsTool
{
    public static async Task Run(InMemoryCollection<Guid, KnowledgeBaseVectorRecord> collection, AzureOpenAIClient client, string model)
    {
        SearchTool searchTool = new SearchTool(collection);

        ChatClientAgent agent = client.GetChatClient(model).CreateAIAgent(
            instructions: "You are an internal knowledge base AI. You tool 'search_internal_knowledge_base' when employee ask you a question",
            tools: [AIFunctionFactory.Create(searchTool.Search, "search_internal_knowledge_base")]);

        while (true)
        {
            Console.Write("> ");
            string input = Console.ReadLine() ?? string.Empty;
            if (input.Equals("q", StringComparison.CurrentCultureIgnoreCase))
            {
                break;
            }

            AgentRunResponse response = await agent.RunAsync(input);
            Console.WriteLine(response);

            Console.WriteLine("---");
            if (response.Usage != null)
            {
                Console.WriteLine($"Input: {response.Usage.InputTokenCount} - Output: {response.Usage.OutputTokenCount}");
            }
        }
    }
}

public class SearchTool(InMemoryCollection<Guid, KnowledgeBaseVectorRecord> collection)
{
    public async Task<List<KnowledgeBaseVectorRecord>> Search(string input)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Using search-tool to get more information");
        Console.ResetColor();

        List<KnowledgeBaseVectorRecord> result = [];
        await foreach (VectorSearchResult<KnowledgeBaseVectorRecord> searchResult in collection.SearchAsync(input, top: 3))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            KnowledgeBaseVectorRecord searchResultRecord = searchResult.Record;
            Console.WriteLine($"- RAG Search Result: Q: {searchResultRecord.Question} - A: {searchResultRecord.Answer} [Score: {searchResult.Score}]");
            Console.ResetColor();
            result.Add(searchResultRecord);
        }

        return result;
    }
}