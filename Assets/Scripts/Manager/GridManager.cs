using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    private List<Food> allSlice = new();
    public float sliceHeight;

    public void GridStart()
    {
        allSlice = new List<Food>(FindObjectsByType<Food>());

        List<float> coordinateX = new();
        List<float> coordinateZ = new();
        foreach (Food ingr in allSlice)
        {
            if(!coordinateX.Contains(ingr.transform.position.x)) coordinateX.Add(ingr.transform.position.x);
            if(!coordinateZ.Contains(ingr.transform.position.z)) coordinateZ.Add(ingr.transform.position.z);
        }
        coordinateX.Sort();
        coordinateZ.Sort();

        List<float> grigliaX = FiltraApprossimati(coordinateX);
        List<float> grigliaZ = FiltraApprossimati(coordinateZ);

        foreach ( var ingr in allSlice)
        {
            int xLogic = FindCloseIndex(grigliaX, ingr.transform.position.x);
            int zLogic = FindCloseIndex(grigliaZ, ingr.transform.position.z);
            ingr.gridPosition = new Vector2Int(xLogic, zLogic);
        }
    }
    private List<float> FiltraApprossimati(List<float> list)
    {
        List<float> filtred = new List<float>();
        
        foreach (float val in list)
        {
            bool close = false;
            foreach (float f in filtred)
            {
                if (Mathf.Abs(val - f) < 0.5f)
                {
                    close = true;
                    break;
                }
            }
            if (!close) filtred.Add(val);
        }
        filtred.Sort();
        return filtred;
    }
    private int FindCloseIndex(List<float> list, float val)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if(Mathf.Abs(list[i] - val) < 0.5f) return i;
        }
        return 0;
    }
    public List<Food> GetPieceInGrid(Vector2Int pos)
    {
        List<Food> foods = new();
        foreach (Food ingr in allSlice)
        {
            if(ingr.gridPosition == pos) foods.Add(ingr);
        }
        foods.Sort((a, b) => a.tPos.y.CompareTo(b.tPos.y));
        return foods;
    }
    public List<Food> GetAllSlices() => allSlice;
}
