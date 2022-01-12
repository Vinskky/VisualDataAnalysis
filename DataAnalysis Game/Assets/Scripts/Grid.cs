using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    public int[,] gridArray;
    //private TextMesh[,] debugArray;

    public Grid(int width, int height, float cellSize, Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        gridArray = new int[width, height];
        //debugArray = new TextMesh[width, height];
        ResetGrid();
        VisualizeGrid();
    }


    public void VisualizeGrid()
    {
        for (int x = 0; x < gridArray.GetLength(0); ++x)
        {
            for(int y= 0; y<gridArray.GetLength(1); ++y)
            {
                //debugArray[x,y] = CreateWorldVisualization(null, gridArray[x, y].ToString(), GetWorldPosition(x, y) + new Vector3(cellSize,1f,cellSize) *0.5f, 8, Color.white, TextAnchor.MiddleCenter, TextAlignment.Center);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.black, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.black, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.black, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.black, 100f);

        //SetValue(1, 1, 50);
    }

    //Create a numbers inside the grid
    public static TextMesh CreateWorldVisualization(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAllignment)
    {
        GameObject gameObj = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObj.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObj.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAllignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        return textMesh;
    }


    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, 1f ,y) * cellSize + originPosition;
    }

    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition- originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).z / cellSize);
    }
    //Set value using grid position
    public void SetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            //debugArray[x, y].text = gridArray[x, y].ToString();
        }
    }
    //Set value giving a world position
    public void SetValue(Vector3 worldposition, int value)
    {
        int x, y;
        GetXY(worldposition, out x, out y);
        SetValue(x, y, value);
    }
    //Get Value from grid position
    public int GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return -1;
        }
    }

    //Get value from world position
    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }

    public void ResetGrid()
    {
        for (int x = 0; x < gridArray.GetLength(0); ++x)
        {
            for (int y = 0; y < gridArray.GetLength(1); ++y)
            {
                gridArray[x, y] = 0;
            }
        }

        GameObject[] elements = GameObject.FindGameObjectsWithTag("HeatmapElement");

        foreach(GameObject el in elements)
        {
            GameObject.Destroy(el);
        }
;    }

}
