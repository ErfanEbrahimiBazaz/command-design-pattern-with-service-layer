using Command1.Business.Commands;

namespace Command1.Business.Invoker;

public class CommandManager
{
    private readonly Stack<ICommand> commands = new Stack<ICommand>();
    private readonly Stack<ICommand> undoneCommands = new Stack<ICommand>();
    //public CommandManager()
    //{
    //    Commands = new Stack<ICommand>();
    //    UndoneCommands = new Stack<ICommand>();
    //}

    public void Undo()
    {
        if (commands.Any())
        {
            var command = commands.Pop();
            command.Undo();
            undoneCommands.Push(command);
        }
    }

    public void Redo() 
    {
        if (undoneCommands.Any())
        {
            var command = undoneCommands.Pop();
            command.Execute();
            commands.Push(command);
        }
    }

    public void ExecuteCommand(ICommand command)
    {
        if(command.CanExecute())
        {
            command.Execute();
            commands.Push(command);
            undoneCommands.Clear();
        }
    }

}
