using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float cellSize = 1f;
    List<Node> grid;
    Vector3 inputDirection;
    Rigidbody itemRigidbody;
    Ray ray;
    RaycastHit hit;
    [SerializeField] LayerMask layerMask;
    void Start()
    {

        itemRigidbody = GetComponent<Rigidbody> ();
        
    }

    void Update()
    {
        grid ??= FindAnyObjectByType<GridManager>().grid;
        inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * 0.25f;
    }

    void FixedUpdate(){
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, layerMask) && grid != null){
            print(hit.collider.name);
            foreach (Node node in grid)
            {
                float distance = Vector3.Distance(hit.point, node.position);
                if (distance < cellSize/2){
                    // print(node.position);
                    transform.position = new(node.position.x, transform.position.y, node.position.z);
                }
            }
        }
        
        // float threshold = 0.1f;

        // if(inputDirection.magnitude != 0)
        // {
        //     itemRigidbody.MovePosition(transform.position + inputDirection);
            
        //     foreach (Node node in grid)
        //     {
        //         transform.position = Vector3.Distance(transform.position, node.position) <= threshold ? node.position : transform.position;
        //     }
        // }
    }
    void OnDrawGizmos(){
        Gizmos.DrawRay(ray.GetPoint(Mathf.Infinity), hit.point);
    }
}
