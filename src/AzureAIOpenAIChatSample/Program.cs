using System.Text;
using Azure;
using Azure.AI.OpenAI;

var client = new OpenAIClient(
    new Uri("https://[YOUR_ID].openai.azure.com/"),
    new AzureKeyCredential("[YOUR_KEY]"));

var options = new ChatCompletionsOptions { MaxTokens = 30000 };

options.Messages.Add(new ChatMessage(ChatRole.System,
    @"You are an AI assistant that helps developers to transform code."));

var fileImput = File.ReadAllText(@"C:\temp\testInput.txt");

var prompt = string.Concat(@"[YOUR_PROMPT]", fileImput, @" ");

options.Messages.Add(new ChatMessage(ChatRole.User, prompt));


var response = await client.GetChatCompletionsStreamingAsync(
    "[YOUR_APP]", options);

var sb = new StringBuilder();
await foreach (var choice in response.Value.GetChoicesStreaming())
await foreach (var message in choice.GetMessageStreaming())
    if (!string.IsNullOrEmpty(message.Content))
        sb.Append(message.Content);

File.WriteAllText(@"C:\temp\testOutput.txt", sb.ToString());