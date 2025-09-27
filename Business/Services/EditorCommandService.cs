using Command1.Business.Commands;
using Command1.Business.Invoker;
using Command1.Business.Receivers;

namespace Command1.Services;
public class EditorCommandService(TextEditor textEditor, CommandManager commandManager)
{
    public void Write(string text)
    {
        var command = new WriteTextCommand(textEditor, text);

        if (!command.CanExecute())
        {
            throw new InvalidOperationException($"Command {command.GetType().Name} cannot be executed.");
        }
        commandManager.ExecuteCommand(command);
    }

    public void DeleteLastNCharacters(int count)
    {
        var command = new DeleteLastNCharactersCommand(textEditor, count);

        if (!command.CanExecute())
        {
            throw new InvalidOperationException($"Command {command.GetType().Name} cannot be executed.");
        }
        commandManager.ExecuteCommand(command);
    }
    public void Undo() { commandManager.Undo(); }
    public void Redo() { commandManager.Redo(); }
    public string GetContent() => textEditor.Content;
}