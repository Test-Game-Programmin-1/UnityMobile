using System.Collections.Generic;
using UnityEngine;
public class CommandManager : MonoBehaviour
{
    public static CommandManager instance;
    private Stack<ICommand> undoStack = new();
    public bool CanUndo = false;
    public bool Moved = false;
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
        undoStack.Clear();
        CanUndo = true;
        Moved = true;
        undoStack.Push(command);
    }
    public void UndoCommand()
    {
        if(undoStack.Count <= 0)
        return;
        undoStack.Pop().UNDO();
        CanUndo = false;
    }
}