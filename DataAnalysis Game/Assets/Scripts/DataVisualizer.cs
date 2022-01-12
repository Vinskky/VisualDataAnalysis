using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataVisualizer : MonoBehaviour
{
    [Header("Grid Settings")]
    public int width;
    public int height;
    public float cellSize;
    public enum dataType { PlayerPosition =1, PlayerDeaths, PlayerHits }

    [Header("Visualization")]
    public dataType showData;
    public bool visualize = false;

    public GameObject heatMapElement;
    public Writer writerAndReader;

    [HideInInspector]
    Grid grid;

    enum HeatMapValues
    {



    }

    private void Start()
    {
        grid = new Grid(width, height, cellSize, gameObject.transform.position);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            visualize = !visualize;
            
        if (!visualize)
            return;
        
        switch(showData)
        {
            case dataType.PlayerPosition:
                DisplayPlayerPosition();
                break;
            case dataType.PlayerDeaths:
                DisplayPlayerDeaths();
                break;
            case dataType.PlayerHits:
                DisplayPlayerHits();
                break;
        }

        DisplayValuesOnMap();

        visualize = false;
    }

    public void DisplayPlayerPosition()
    {
        //Read Player position
        List<EventTrackPlayerPosition> playerPositionEvents = writerAndReader.DeserilizePlayerPosEvent();

        foreach (EventTrackPlayerPosition ev in playerPositionEvents)
        {
            int test = grid.GetValue(ev.position);
            grid.SetValue(ev.position, test + 1);
        }

    }

    public void DisplayPlayerDeaths()
    {
        //Read Player Deaths
        List<EventPlayerDead> playerPosistionDeaths = writerAndReader.DeserilizePlayerDeadEvent();

        foreach (EventPlayerDead ev in playerPosistionDeaths)
        {
            grid.SetValue(ev.position, grid.GetValue(ev.position) + 1);
        }
    }

    public void DisplayPlayerHits()
    {
        //Read Player Hits
        List<EventPlayerHit> playerHits = writerAndReader.DeserilizePlayerHitEvent();

        foreach (EventPlayerHit ev in playerHits)
        {
            grid.SetValue(ev.position, grid.GetValue(ev.position) + 1);
        }
    }

    public void DisplayValuesOnMap()
    {
        for(int x =0; x < grid.gridArray.GetLength(0); ++x)
        {
            for (int y = 0; y < grid.gridArray.GetLength(1); ++y)
            {
                if(grid.gridArray[x,y] > 0)
                {
                    Instantiate(heatMapElement, grid.GetWorldPosition(x, y), Quaternion.identity);
                    Debug.Log(grid.gridArray[x, y]);
                }
            }
        }
    }
}
