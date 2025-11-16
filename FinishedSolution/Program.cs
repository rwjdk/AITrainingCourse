using System.ClientModel;
using Azure.AI.OpenAI;
using FinishedSolution;
using Microsoft.Extensions.Configuration;

/* Exercise 0: Get your Resources Ready
 1. Get you Azure AI Foundry Project, and get Endpoint (The Azure OpenAI version: https://<yourname>.openai.azure.com/) and API Key (normally an 84 char long string)
 2. Deploy a chat-model: gpt-4.1-mini model in AI Foundry
 2. Deploy an embedding-model: text-embedding-3-small in AI Foundry
 3. Store apiKey in UserSecrets (Alternative just a string if security is not a concern) + endpoint and model name
 */
const string endpoint = "https://training-rwj-resource.openai.azure.com/";
const string model = "gpt-4.1-mini";
string apiKey = new ConfigurationBuilder().AddUserSecrets<Program>().Build()["apiKey"];

/* Exercise 1: Nuget Packages + new AzureOpenAIClient
 1. Add NuGet Package 'Microsoft.Agents.AI.OpenAI' + 'Azure.AI.OpenAI' (both: latest prerelease)
 2. Create a new instance of the AzureOpenAiClient (namespace: Azure.AI.OpenAI)
 */

AzureOpenAIClient client = new AzureOpenAIClient(new Uri(endpoint), new ApiKeyCredential(apiKey));

await Exercise2HelloWorld.Run(client, model);
//await Exercise3TheChatLoop.Run(client, model);
//await Exercise4Tools.Run(client, model);
//await Exercise5StructuredOutput.Run(client, model);
//await Exercise6Rag.Run(client, model);

Console.WriteLine();
Console.WriteLine("Done");