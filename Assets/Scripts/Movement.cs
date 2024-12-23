using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float cellSize = 1f;
    List<Node> grid;
    Vector3 inputDirection;
    Ray ray;
    RaycastHit hit;
    [SerializeField] LayerMask layerMask;
    void Start()
    {
    }

    void Update()
    {
        grid ??= FindAnyObjectByType<GridManager>().grid;
        inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * 0.25f;
    }

    void FixedUpdate(){
        if(Input.GetMouseButton(0)){
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask: layerMask) && grid != null){
                foreach (Node node in grid)
                {
                    float distance = Vector3.Distance(hit.point, node.position);
                    if (distance < cellSize/2 && !node.occupied){
                        transform.position = new(node.position.x, transform.position.y, node.position.z);
                    }
                }
            }
        }
    }
    void OnDrawGizmos(){
        Gizmos.DrawRay(ray.GetPoint(Mathf.Infinity), hit.point);
    }
}
