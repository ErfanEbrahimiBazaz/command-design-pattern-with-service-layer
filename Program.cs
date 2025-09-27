using Command1;
using Command1.Services;
using Microsoft.Extensions.DependencyInjection;
// approach 3: with DI and service layer

IServiceCollection services = new ServiceCollection();
services.AddCommandPatternDI();
var provider = services.BuildServiceProvider();

EditorCommandService editorCommandService = provider.GetRequiredService<EditorCommandService>();

editorCommandService.Write("Hello, ");
Console.WriteLine("Text editor content:");
Console.WriteLine(editorCommandService.GetContent());
editorCommandService.Write("World!");
Console.WriteLine(editorCommandService.GetContent());
editorCommandService.Undo();
Console.WriteLine(editorCommandService.GetContent());
editorCommandService.Undo();
Console.WriteLine(editorCommandService.GetContent());
editorCommandService.Redo();
Console.WriteLine(editorCommandService.GetContent());
editorCommandService.Redo();
Console.WriteLine(editorCommandService.GetContent());
editorCommandService.DeleteLastNCharacters(5);
Console.WriteLine(editorCommandService.GetContent());