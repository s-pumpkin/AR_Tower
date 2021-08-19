using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class GameUISetting : MonoBehaviour
{
    private static GameUISetting _instance;
    public static GameUISetting instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameUISetting>();
            }
            return _instance;
        }
    }
    private bool scanPlaneBool = true;
    public GameObject 掃描平面UI;
    public GameObject 場景設置確定按鈕;

    public GameObject 選擇平面UI;
    public ChoosePlane choosePlane;
    public GameObject 擺放初始物件UI;
    public GameObject GameUI;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        scanPlaneOK();
    }

    private void NavBack()
    {
        NavMeshSurface[] navMeshSurfaces = FindObjectsOfType<NavMeshSurface>();
        foreach (NavMeshSurface Nav in navMeshSurfaces)
        {
            if (Nav != null)
            {
                Nav.BuildNavMesh();
            }
        }
    }

    //場景確認、AR掃描暫停、擺放初始物件
    void scanPlaneOK()
    {
        if (scanPlaneBool)
        {
            if (GameObject.FindGameObjectWithTag("Ground"))
            {
                場景設置確定按鈕.SetActive(true);
                return;
            }
            場景設置確定按鈕.SetActive(false);
        }
    }

    public void 場地掃描完成()
    {
        掃描平面UI.SetActive(false);
        //StopDetection.instance.Stop(); //PC測是要把這註解
        GameObject[] Plane = GameObject.FindGameObjectsWithTag("Ground");
        Debug.Log(Plane.Length);
        if (Plane.Length > 1)
        {
            選擇平面Open();
            scanPlaneBool = false;
            return;
        }
        NavBack();
        擺放初始物件UIOpen();
        scanPlaneBool = false;
    }

    void 選擇平面Open()
    {
        選擇平面UI.SetActive(true);
        choosePlane.enabled = true;
    }

    public void 確認選擇平面Close()
    {
        if (choosePlane.Choosed == null) return;
        choosePlane.DestroyOtherGameObject();
        choosePlane.enabled = false;
        選擇平面UI.SetActive(false);
        Invoke("NavBack", 1f);
        擺放初始物件UIOpen();
    }

    void 擺放初始物件UIOpen()
    {
        擺放初始物件UI.SetActive(true);
    }

    public void 前置作業布置完成()
    {
        if (GameObject.FindGameObjectWithTag("Tower") != null && GameObject.FindGameObjectWithTag("MonsterRebirth") != null)
        {
            擺放初始物件UI.SetActive(false);
            GameUI.SetActive(true);
        }
    }

}
