using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GridManager grid;
    [SerializeField] float Timer;
    List<Food> allSlice = new();
    [SerializeField] List<Food> destinationStack = new();
    void Awake()
    {
        grid = FindAnyObjectByType<GridManager>();
        
        grid.GridStart();
    }
    void Start()
    {
        allSlice = grid.GetAllSlices();
    }

    public void TryMove(Vector2Int origin, Vector2Int direction)
    {
        Vector2Int destination = origin + direction;
        List<Food> originalStack = grid.GetPieceInGrid(origin);
        List<Food> destinationStack = grid.GetPieceInGrid(destination);
        if (originalStack.Count == 0 || destinationStack.Count == 0) return;
        if (originalStack[0].type == FoodSlices.Bred && originalStack.Count == 1) return;

        bool moveBread = false;
        foreach (Food slice in originalStack)
        {
            if(slice.type == FoodSlices.Bred)
            {
                moveBread = true;
            }
        }
        if (moveBread)
        {
            if (!moveBreadAndSlice())
            {
                return;
            }
        }
        Vector3 anchor = destinationStack[0].transform.position;
        int stackHeight = destinationStack.Count;
        float Height = grid.sliceHeight;

        List<Food> reverseStack = new(originalStack);
        reverseStack.Reverse();
        for (int i = 0; i < reverseStack.Count; i++) reverseStack[i].Flip(destination, anchor, stackHeight, i, direction, Height);

        MoveCommand NewMove = new(originalStack, origin, destination, direction, anchor, stackHeight, Height);

        Win();
    }
    bool moveBreadAndSlice()
    {
        List<Vector2Int> FoodPos = new();
        foreach(Food slice in allSlice)
        {
            List<Food> stack = grid.GetPieceInGrid(slice.gridPosition);
            if(stack[0].type != FoodSlices.Bred) return false;
        }
        return true;
    }
    void Win()
    {
        Debug.Log("1");
        if(allSlice.Count == 0) return;
        Debug.Log("2");
        Vector2Int pos = allSlice[0].gridPosition;

        bool AllSamePos = true;
        foreach(Food slice in allSlice)
        {
            if(slice.gridPosition != pos)
            {
                Debug.Log("3");
                AllSamePos = false;
            }
        }
        if(!AllSamePos) return;
        Debug.Log("4");
        List<Food> Stack = grid.GetPieceInGrid(pos);
        destinationStack = Stack;
        if(Stack[0].type == FoodSlices.Bred && Stack[Stack.Count - 1].type == FoodSlices.Bred)
        {
            Debug.Log("5");
            StartCoroutine(WinTiming());
        }
        
    }
    IEnumerator WinTiming()
    {
        Debug.Log("TimerUp");
        yield return new WaitForSeconds(Timer);
        Debug.Log("TimerDown");
        UIManager ui = FindAnyObjectByType<UIManager>();
        if(ui != null)
        {
            ui.Win();
        }
    }
}