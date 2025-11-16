using Azure.AI.OpenAI;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel.Connectors.InMemory;

namespace FinishedSolution;

public class Exercise6RagVectorStore
{
    public static async Task Run(List<KnowledgeBaseEntry> knowledgeBase, AzureOpenAIClient client, string model)
    {
        Console.WriteLine("- VECTOR DATA UPFRONT -");
        IEmbeddingGenerator<string, Embedding<float>> embeddingGenerator = client
            .GetEmbeddingClient("text-embedding-3-small")
            .AsIEmbeddingGenerator();

        InMemoryVectorStore vectorStore =
            new(new InMemoryVectorStoreOptions
            {
                EmbeddingGenerator = embeddingGenerator
            });

        #region Alternatives

        //Microsoft.SemanticKernel.Connectors.AzureAISearch.AzureAISearchVectorStore vectorStoreFromAzureAiSearch = new AzureAISearchVectorStore(
        //    new SearchIndexClient(new Uri("azureAiSearchEndpoint"),
        //        new AzureKeyCredential("azureAiSearchKey")
        //    ));

        //Microsoft.SemanticKernel.Connectors.SqlServer.SqlServerVectorStore vectorStoreFromSqlServer2025 = new SqlServerVectorStore(
        //    "connectionString");

        //Microsoft.SemanticKernel.Connectors.CosmosNoSql.CosmosNoSqlVectorStore vectorStoreFromCosmosDb = new CosmosNoSqlVectorStore(
        //    "connectionString",
        //    "databaseName",
        //    new CosmosClientOptions
        //    {
        //        UseSystemTextJsonSerializerWithOptions = JsonSerializerOptions.Default,
        //    });

        #endregion

        InMemoryCollection<Guid, KnowledgeBaseVectorRecord> collection = vectorStore.GetCollection<Guid, KnowledgeBaseVectorRecord>("knowledge");
        await collection.EnsureCollectionExistsAsync();

        int counter = 0;
        foreach (KnowledgeBaseEntry entry in knowledgeBase)
        {
            counter++;
            Console.Write($"\rEmbedding Data: {counter}/{knowledgeBase.Count}");
            await collection.UpsertAsync(new KnowledgeBaseVectorRecord
            {
                Id = Guid.NewGuid(),
                Question = entry.Question,
                Answer = entry.Answer,
            });
        }

        Console.WriteLine();
        Console.WriteLine("\rEmbedding complete... Let's ask the question again using RAG");

        await Exercise6RagVectorStoreUpfront.Run(collection, client, model);
        //await Exercise6RagVectorStoreAsTool.Run(collection, client, model);
    }
}

public class KnowledgeBaseVectorRecord
{
    [VectorStoreKey]
    public required Guid Id { get; set; }

    [VectorStoreData]
    public required string Question { get; set; }

    [VectorStoreData]
    public required string Answer { get; set; }

    [VectorStoreVector(1536, DistanceFunction = DistanceFunction.CosineSimilarity, IndexKind = IndexKind.Flat)]
    public string Vector => $"Q: {Question} - A: {Answer}";
}