using Azure.AI.OpenAI;

namespace FinishedSolution;

public static class Exercise6Rag
{
    public static async Task Run(AzureOpenAIClient client, string model)
    {
        /* Exercise 6: RAG
         1. Create a set of Knowledge Base Entry (Q&A) objects as a list (Minimum 10) about some personal or internal company stuff
         2. Feed the AI all the Knowledge upfront and ask it questions about it in a chat loop (notice the Token usage)
         3. Add NuGet Package 'Microsoft.SemanticKernel.Connectors.InMemory' (Pre-release)
         4. Embed the knowledge base
         5. Search it (Upfront)
         5. Search it (As a Tool)
        */

        //Create the raw data (that in a real setup would be in some real datastore)
        List<KnowledgeBaseEntry> knowledgeBase =
        [
            new("What is the guest Wifi Password?", "The guest wifi password is '123qwe' (all lowercase letters)"),
            new("Is Christmas Eve a full or half day off", "It is a full day off"),
            new("How do I register vacation?", "Go to the internal portal and under Vacation Registration (top right), enter your request. Your manager will be notified and will approve/reject the request"),
            new("What do I need to do if I'm sick?", "Inform you manager, and if you have any meetings remember to tell the affected colleagues/customers"),
            new("Where is the employee handbook?", "It is located [here](https://www.yourcompany.com/hr/handbook.pdf)"),
            new("Who is in charge of support?", "John Doe is in charge of support. His email is john@yourcompany.com"),
            new("I can't log in to my office account", "Take hold of Susan. She can reset your password"),
            new("When using the CRM System if get error 'index out of bounds'", "That is a known issue. Log out and back in to get it working again. The CRM team have been informed and status of ticket can be seen here: https://www.crm.com/tickets/12354"),
            new("What is the policy on buying books and online courses?", "Any training material under 20$ you can just buy.. anything higher need an approval from Richard"),
            new("Is there a bounty for find candidates for an open job position?", "Yes. 1000$ if we hire them... Have them send the application to jobs@yourcompany.com")
        ];

        await Exercise6RagUpfrontRaw.Run(knowledgeBase, client, model);
        //await Exercise6RagVectorStore.Run(knowledgeBase, client, model);
    }
}

public record KnowledgeBaseEntry(string Question, string Answer);