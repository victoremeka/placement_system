using System;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Vector2 gridDimension;
    [SerializeField]private float cellSize;
    public GameObject tile;
    public GameObject parentTile;
    List<Node> grid;

    void Start(){
        if (cellSize != 0)
        {
            grid = new List<Node>();
            CreateGrid();
        }
    }

    void Update(){
        grid = new List<Node>();
        CreateGrid();
        
    }

    void CreateGrid(){
        float halfGridX = gridDimension.x/2;
        float halfGridY = gridDimension.y/2;
        Vector3 worldOffset = transform.position - Vector3.right * halfGridX - Vector3.forward * halfGridY;

        for (float x = 0; x < gridDimension.x; x++)
        {
            for (float y = 0; y < gridDimension.y; y++)
            {
                if(x%cellSize == 0 && y%cellSize == 0){
                    Vector3 nodePosition = worldOffset + Vector3.right * (x + cellSize/2) + Vector3.forward * (y + cellSize/2) ;
                    bool occupied = Physics.CheckSphere(nodePosition, cellSize/2);
                    Node node = new(nodePosition,occupied);
                    grid.Add(node);
                }
                
            }
        }
    }

    void OnDrawGizmos(){
        Gizmos.DrawWireCube(transform.position, new Vector3(gridDimension.x, 1, gridDimension.y));
        if(grid != null){
            foreach (Node node in grid){
                if (node != null){
                    tile.name = node.position.ToString();
                    Gizmos.DrawWireSphere(node.position, cellSize/4);
                }
            }
        }
    }
}
