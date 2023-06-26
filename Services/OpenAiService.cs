using Microsoft.Extensions.Options;
using OpenAI_API.Completions;
using OpenAI_API.Models;
using OpenAIApp.Configurations;

namespace OpenAIApp.Services;

public class OpenAiService : IOpenAiService
{
    private readonly OpenAiConfig _openAiConfig;

    public OpenAiService(IOptions<OpenAiConfig> openAiConfigOptions)
    {
        _openAiConfig = openAiConfigOptions.Value;
    }

    public async Task<string> CheckProgrammingLanguage(string code)
    {
        var api = new OpenAI_API.OpenAIAPI(_openAiConfig.ApiKey);
        var chat = api.Chat.CreateConversation();
        chat.AppendSystemMessage(
            @"You are a teacher who helps new programmer understand if some text is a programming language or not.
            If the input is a programming language, you should reply with 'Yes', otherwise you should reply with 'No'.");

        chat.AppendUserInput(code);

        var respose = await chat.GetResponseFromChatbotAsync();

        return respose;
    }

    public async Task<string> GetAdvancedCompletionAsync(string prompt)
    {
        var api = new OpenAI_API.OpenAIAPI(_openAiConfig.ApiKey);
        var result = await api.Completions.CreateCompletionAsync(
            new CompletionRequest(
                prompt: prompt, 
                model: Model.CurieText,
                temperature: 0.9));

        return result.Completions[0].Text;

    }

    public async Task<string> GetCompletionAsync(string prompt)
    {
        var api = new OpenAI_API.OpenAIAPI(_openAiConfig.ApiKey);
        var result = await api.Completions.GetCompletion(prompt);
        return result;
    }
} 