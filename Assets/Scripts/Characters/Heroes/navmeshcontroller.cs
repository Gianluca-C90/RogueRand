using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class navmeshcontroller : MonoBehaviour
{
    public Transform target;
    NavMeshPath path;

    // Start is called before the first frame update
    void Start()
    {
        path = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);
        GetComponent<NavMeshAgent>().SetPath(path);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
