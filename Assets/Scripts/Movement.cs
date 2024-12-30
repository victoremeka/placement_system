using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float cellSize = 1f;
    List<Node> grid;
    Vector3 inputDirection;
    Ray ray;
    RaycastHit hit;
    [SerializeField] LayerMask layerMask;
    LayerMask draggableLayerMask, itemLayerMask, OriginalLayerMask;
    void Start()
    {
        draggableLayerMask = LayerMask.NameToLayer("Draggable");
        itemLayerMask = LayerMask.GetMask("Item");
        OriginalLayerMask = LayerMask.NameToLayer("Item");
    }

    void Update()
    {
        grid ??= FindAnyObjectByType<GridManager>().grid;
        inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * 0.25f;
    }

    Transform draggable;
    bool itemSelected = false;
    void FixedUpdate(){
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if(Input.GetMouseButton(1) && itemSelected){
            draggable.gameObject.layer = OriginalLayerMask;
            draggable = null;
            itemSelected = false;
        }
        if(Input.GetMouseButton(0)){
            if (!itemSelected && Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask: itemLayerMask))
            {
                draggable = hit.transform;
                draggable.gameObject.layer = draggableLayerMask;
                itemSelected = true;
            }
               if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask: layerMask) && grid != null && draggable != null)
                {    
                    foreach (Node node in grid)
                    { 
                        float distance = Vector3.Distance(hit.point, node.position);
                        if (distance < cellSize/2 && !node.occupied){
                            AvoidCollision(node);
                            draggable.position = new(node.position.x, draggable.position.y, node.position.z);
                        }
                    }
                }
        }
    }

    void AvoidCollision(Node node){
        Vector3 targetScale = draggable.localScale;
        Vector3 targetDimensions = draggable.localPosition + targetScale;    
        print($"{targetDimensions} - {node.position}");
    }

    void OnDrawGizmos(){
        Gizmos.DrawLine(ray.origin, hit.point);
    }
}
