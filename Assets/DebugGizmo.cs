using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGizmo : MonoBehaviour
{

    public float angleHorizontal;
    public float angleVertical;
    public float radius = 10;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);

        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, Quaternion.AngleAxis(angleHorizontal, transform.up) * transform.forward * radius);
        Gizmos.DrawRay(transform.position, Quaternion.AngleAxis(-angleHorizontal, transform.up) * transform.forward * radius);

        Gizmos.DrawRay(transform.position, Quaternion.AngleAxis(angleVertical, transform.right) * transform.forward * radius);
        Gizmos.DrawRay(transform.position, Quaternion.AngleAxis(-angleVertical, transform.right) * transform.forward * radius);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, player.transform.position);

    }
}
