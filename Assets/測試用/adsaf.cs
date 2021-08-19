using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adsaf : MonoBehaviour
{

    public GameObject aa;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);  //滑鼠測試可以
                                                                        //Ray r = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(r, out hit, Mathf.Infinity))
            {
                if (hit.transform.gameObject.tag == "Ground")
                {
                    aa = hit.transform.gameObject;
                }
            }
        }
    }

    public void Destroy()
    {
        GameObject[] bb = GameObject.FindGameObjectsWithTag("Ground");
        foreach (GameObject asd in bb)
        {
            if (asd != aa)
            {
                Destroy(asd);
            }
        }
    }
}
