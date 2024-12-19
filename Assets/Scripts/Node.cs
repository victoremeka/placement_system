using UnityEngine;

public class Node
{
    public Vector3 position;
    public bool occupied;

    public Node(Vector3 position, bool occupied){
        this.position = position;
        this.occupied = occupied;
    }
}
