using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Items to be placed on grid have to be divisible by gridDimension
    [SerializeField] LayerMask layerMask;
    [SerializeField] private Vector2 gridDimension;
    float cellSize = 1f;
    public GameObject tile;
    [HideInInspector] public List<Node> grid;

    GameObject[] items;

    void Start(){
        if (cellSize != 0)
        {
            grid = new List<Node>();
            items = GameObject.FindGameObjectsWithTag("Item");
            CreateGrid();
        }
    }

    void FixedUpdate(){
        foreach (Node node in grid)
            node.occupied = Physics.CheckSphere(node.position, cellSize/4, layerMask);    
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
                    bool occupied = Physics.CheckSphere(nodePosition, cellSize/4, layerMask);
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
                        Gizmos.color = node.occupied ? Color.red : Color.green;
                    
                    Gizmos.DrawWireSphere(node.position, cellSize/4);
                }
            }
        }
    }
}
