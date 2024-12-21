using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] private Vector2 gridDimension;
    [SerializeField] float cellSize;
    public GameObject tile;
    List<Node> grid;

    

    GameObject[] items;

    void Start(){
        if (cellSize != 0)
        {
            grid = new List<Node>();
            items = GameObject.FindGameObjectsWithTag("Item");
            CreateGrid();
        }
    }

    void Update(){
        foreach (Node node in grid)
            node.occupied = Physics.CheckSphere(node.position, cellSize/2, layerMask);
        IEnumerator coroutine = PlaceItem(items[0]);
        if(Input.GetKey(KeyCode.Space)){
            StartCoroutine(coroutine);
        } else if (Input.GetKey(KeyCode.S))
        {
            StopCoroutine(coroutine);
        }
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
                    bool occupied = Physics.CheckSphere(nodePosition, cellSize/2, layerMask);
                    Node node = new(nodePosition,occupied);
                    grid.Add(node);
                }
                
            }
        }
    }

    IEnumerator PlaceItem(GameObject item){
        float threshold = cellSize/2;
        foreach (Node node in grid){
            Vector3 distanceBetween = node.position - item.transform.localScale * threshold;
            // set item position to nearest node position
        }
        yield return null;
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
