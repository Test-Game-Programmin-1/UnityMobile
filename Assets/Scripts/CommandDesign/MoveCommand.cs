using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveCommand : ICommand
{
    List<Food> move;
    Vector2Int originalPos;
    Vector2Int newPos;
    Vector2Int direction;
    Vector3 AnchorPos;
    Vector3 AnchorOldPos;
    int HeightDest;
    float HeightFood;
    public MoveCommand(List<Food> _move, Vector2Int originalPos, Vector2Int newPos, Vector2Int direction, Vector3 AnchorPos, int HeightDest, float HeightFood)
    {
        this.move = _move;
        this.originalPos = originalPos;
        this.newPos = newPos;
        this.direction = direction;
        this.AnchorPos = AnchorPos;
        this.HeightDest = HeightDest;
        this.HeightFood = HeightFood;
        if(_move.Count > 0)
        {
            AnchorOldPos = _move[0].transform.position;
        }
        CommandManager.instance.AddCommand(this);
    }
    public void UNDO()
    {
        GridManager grid = Object.FindAnyObjectByType<GridManager>();
        List<Food> oldSlice = grid.GetPieceInGrid(originalPos);

        Vector3 BackAnchor;
        int BackHeight = 0;
        if(oldSlice.Count > 0)
        {
            BackAnchor = oldSlice[0].tPos;
            BackHeight = oldSlice.Count;
        }
        else
        {
            BackAnchor = AnchorOldPos;
            BackAnchor.y = 0;
        }
        Vector2Int inverseDirection = -direction;
        for(int i = 0; i < move.Count; i++)
        {
            move[i].Flip(originalPos, BackAnchor, BackHeight, i, inverseDirection, HeightFood);
        }
        
    }
}
