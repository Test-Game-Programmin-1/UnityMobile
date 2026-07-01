using UnityEngine;

public interface ICommand
{
    public void Execute();
    public void UNDO();
}
