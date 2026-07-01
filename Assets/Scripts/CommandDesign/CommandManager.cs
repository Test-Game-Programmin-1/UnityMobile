using System.Collections.Generic;
using UnityEngine;
public class CommandManager : MonoBehaviour
{
    public static CommandManager instance;
    private Stack<ICommand> undoStack = new();
    private Stack<ICommand> redoStack = new();

    void Awake()
    {
        if(instance != null)
        {
            Destroy(instance);
            return;
        }
        instance = this;
    }
    public void AddCommand(ICommand command)
    {
        if(redoStack.Count > 0)
        {
            redoStack.Clear();
        }
        undoStack.Push(command);

        command.Execute();
    }
    public void UndoCommand()
    {
        if(redoStack.Count <= 0)
        return;

        undoStack.Push(redoStack.Peek());
        redoStack.Pop().Execute();
    }
    public void ClearCommands()
    {
        undoStack.Clear();
        redoStack.Clear();
    }
}