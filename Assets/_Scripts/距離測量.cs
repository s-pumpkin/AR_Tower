using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 距離測量 : MonoBehaviour
{
    public GameObject a;
    public GameObject b;
    private float Distance;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Distance = Vector3.Distance(a.transform.position, b.transform.position);
        Debug.Log("目前距離：" + Distance);
    }
}
