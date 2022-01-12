using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataVisualizer : MonoBehaviour
{
    [Header("Grid Settings")]
    public int width;
    public int height;
    public float cellSize;
    public enum dataType { PlayerPosition, PlayerDeaths, PlayerHits }

    [Header("Visualization")]
    public dataType showData;
    public bool visualize = false;
    

    private void Start()
    {
        Grid grid = new Grid(width, height, cellSize, gameObject.transform.position);
    }

    private void Update()
    {
        
    }
}
