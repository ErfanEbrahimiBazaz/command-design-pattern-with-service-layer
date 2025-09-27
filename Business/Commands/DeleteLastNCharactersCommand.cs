using Command1.Business.Receivers;

namespace Command1.Business.Commands;

public class DeleteLastNCharactersCommand : ICommand
{
    private readonly TextEditor textEditor;
    private readonly int count;

    public DeleteLastNCharactersCommand(TextEditor textEditor, int count )
    {
        this.textEditor = textEditor;
        this.count = count;
    }

    private string RemovedNCharacters { get; set; }
    public bool CanExecute()
    {
        if(count <= 0 || count > textEditor.Content.Length)
        {
            return false;
        }
        return true;
    }

    public void Execute()
    {
        RemovedNCharacters = textEditor.Content.Substring(textEditor.Content.Length - count, count);
        textEditor.DeleteLastNCharacters(count);
    }

    public void Undo()
    {
        textEditor.Write(RemovedNCharacters);
    }
}
