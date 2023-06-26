namespace OpenAIApp.Services;

public interface IOpenAiService
{
    Task<string> GetCompletionAsync(string prompt);
    Task<string> GetAdvancedCompletionAsync(string prompt);
    Task<string> CheckProgrammingLanguage(string code);
}