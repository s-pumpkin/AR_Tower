using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


public class ChoosePlane : MonoBehaviour
{
    public Material OutlineMater;
    public Material FloorMaterial;

    public GameObject Choosed;
    public GameObject 選擇遊玩場地Button;
    
    //public GameObject NoNeedObject = new GameObject("NoNeedObject");

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.gameObject.tag == "Ground")
                {
                    if (Choosed != null)
                    {
                        Choosed.GetComponent<MeshRenderer>().sharedMaterial = FloorMaterial;
                    }
                    Choosed = hit.collider.gameObject; //打到的物件為選取的物件
                    Choosed.GetComponent<MeshRenderer>().sharedMaterial = OutlineMater; //MeshRenderer.Material 必須為2個以上 ,sharedMaterial會更改第1個材質
                    if (Choosed != null && 選擇遊玩場地Button != null)
                    {
                        選擇遊玩場地Button.SetActive(true);
                    }
                }
            }
        }
    }

    public void DestroyOtherGameObject()
    {
        Choosed.GetComponent<MeshRenderer>().sharedMaterial = FloorMaterial;        
        GameObject[] bb = GameObject.FindGameObjectsWithTag("Ground");
        foreach (GameObject asd in bb)
        {
            if (asd != Choosed)
            {
                Destroy(asd);
            }
        }
    }
}
