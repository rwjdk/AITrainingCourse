/* Exercise 0: Get your Resources Ready
 1. Get you Azure AI Foundry Project, and get Endpoint (The Azure OpenAI version: https://<yourname>.openai.azure.com/) and API Key (normally an 84 char long string)
 2. Deploy a gpt-4.1-mini model in AI Foundry
 3. Store apiKey in UserSecrets (Alternative just a string if security is not a concern) + endpoint and model name
 */

//todo

/* Exercise 1: Nuget Packages + new AzureOpenAIClient
 1. Add NuGet Package 'Microsoft.Agents.AI.OpenAI' + 'Azure.AI.OpenAI' (both: latest prerelease)
 2. Create a new instance of the AzureOpenAiClient (namespace: Azure.AI.OpenAI)
 */

//todo

/* Exercise 2: Hello World
 1. From the client get a ChatClient with the gpt-4.1-mini model and convert it into an AIAgent (ChatClientAgent) using CreateAIAgent()
 2. Ask the agent a question using RunAsync(...)
 3. Print the answer in the console
 4. (Bonus) Make it a streaming response instead
 5. (Bonus) Give you Agent rules and personality using the optional "instructions" parameter you can give to CreateAIAgent(...)
 6. (Bonus) Find out how many tokens you use in input and output
 */

//todo

/* Exercise 3: The Chat Loop
 1. Let's make a chat loop so we can input questions
 2. Let's add a AgentThread so it is a continuous conversation
 */

//todo

/* Exercise 4: Tool Calling
 1. Give your agent an Information Tool (example a tool that based on name can give info about a person given a name as input)
 2. Make an Action Tool (example a tool that change the color of the text in the Console)
 */

//todo

/* Exercise 5: Structured Output
 1. Get the Agent to output what IMDB's Top 3 movies are
 2. Use instructions to guide the AI to follow a specific format of the output (Title, Director, Year of Release and Score)
 3. Now do it as structured output instead to have better control what data comes back
*/

//todo

/* Exercise 6: RAG
 1. Create a set of Knowledge Base Entry (Q&A) objects as a list (Minimum 10) about some personal or internal company stuff
 2. Feed the AI all the Knowledge upfront and ask it questions about it in a chat loop (notice the Token usage)
 3. Add NuGet Package 'Microsoft.SemanticKernel.Connectors.InMemory' (Pre-release)
 4. Embed the knowledge base
 5. Search it (Upfront)
 5. Search it (As a Tool)
*/

Console.WriteLine();
Console.WriteLine("Done");