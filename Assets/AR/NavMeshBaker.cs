using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NavMeshBaker : MonoBehaviour
{
    [SerializeField]
    NavMeshSurface navMeshSurfaces;

    [SerializeField]
    GameObject[] gameobjectnavMeshSurfaces;
    public GameObject cube;
    public Transform planes;
    public int Obj_planesChildCount = 0;
    [SerializeField] Text Count;
    void Start()
    {



    }

    public void a6454a564()
    {
        GameObject[] a = GameObject.FindGameObjectsWithTag("Plane");
        foreach (GameObject b121 in a)
        {
            GameObject b = (GameObject)Instantiate(cube, transform.position, Quaternion.identity, planes);

        }
        navMeshSurfaces = FindObjectOfType<NavMeshSurface>();
        navMeshSurfaces.BuildNavMesh();
        Obj_planesChildCount = planes.transform.childCount;
            Count.text =Obj_planesChildCount.ToString("0000");
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            /*
            gameobjectnavMeshSurfaces = GameObject.FindGameObjectsWithTag("Plane");
            navMeshSurfaces = FindObjectOfType<NavMeshSurface>();
            for (int i = 0; i < gameobjectnavMeshSurfaces.Length; i++)
            {
                navMeshSurfaces.BuildNavMesh();
            }
            */
            GameObject[] a = GameObject.FindGameObjectsWithTag("Plane");
            foreach (GameObject b121 in a)
            {
                GameObject b = (GameObject)Instantiate(cube, transform.position, Quaternion.identity, planes);
                
            }
            navMeshSurfaces = FindObjectOfType<NavMeshSurface>();
            navMeshSurfaces.BuildNavMesh();
            Obj_planesChildCount = planes.transform.childCount;
            Count.text =Obj_planesChildCount.ToString("0000");
        }

    }
}
