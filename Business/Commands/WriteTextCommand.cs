using Command1.Business.Receivers;

namespace Command1.Business.Commands;

public class WriteTextCommand : ICommand
{
    private readonly TextEditor textEditor;
    private readonly string text = string.Empty;

    public WriteTextCommand(TextEditor textEditor, string text)
    {
        this.textEditor = textEditor;
        this.text = text;
    }
    public bool CanExecute()
    {
        if(string.IsNullOrWhiteSpace(text))
        {
            return false;
        }
        return true;
    }

    public void Execute()
    {
        textEditor.Write(text);
    }

    public void Undo()
    {
        textEditor.DeleteLastNCharacters(text.Length);
    }
}
