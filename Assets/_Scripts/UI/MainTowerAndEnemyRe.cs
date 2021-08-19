using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class MainTowerAndEnemyRe : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerClickHandler
{
    public float interval = 0.1f;

    [SerializeField]
    UnityEvent m_OnLongpress = new UnityEvent();
    private bool isPointDown = false;
    private float lastInvokeTime;

    bool LongPress = false; //是否長按
    bool ExitButton = false; // 是否離開按鈕
    public bool OnceTime = true; //是否生成一次

    public Button Button;
    public bool 限制數量 = false;
    public int 限制數量Value = 1;
    private int value = 0;
    private GameObject T;
    public GameObject 生成GameObject;
    private GameObject Plane;
    void Start()
    {

    }

    void Update()
    {
        if (isPointDown) //是否長按
        {
            if (Time.time - lastInvokeTime > interval)
            {
                //触发点击;    
                m_OnLongpress.Invoke();
                lastInvokeTime = Time.time;
                Debug.Log("长按");
                //----------------------
            }
        }

        if (限制數量)
        {
            if (value == 限制數量Value && OnceTime)
            {
                Button.interactable = false;
            }
        }
        CreateTower();
        //Debug.Log("UIOnClickTest--------------召喚:" + T);
    }

    //先按下 離開的同時是否長按 生成一個物件 放開後在手放開的位置生成物件 並把 手指按住的刪除
    void CreateTower()
    {
        if (LongPress && ExitButton && Button.interactable)    //生成塔
        {
            if (Plane == null)
            {
                Plane = GameObject.FindGameObjectWithTag("Ground");
            }

            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);  //滑鼠測試可以
                                                                        //Ray r = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(r, out hit, Mathf.Infinity))
            {

                if (hit.transform.gameObject.tag == "Ground")
                {
                    if (OnceTime)
                    {
                        OnceTime = false;
                        T = (GameObject)Instantiate(生成GameObject);
                        T.gameObject.transform.parent = Plane.gameObject.transform;
                        value += 1;
                        Debug.Log(value);
                    }

                    T.transform.position = hit.point;
                }
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //原本就有的
        m_OnLongpress.Invoke();
        isPointDown = true;
        lastInvokeTime = Time.time;
        Debug.Log("鼠标按下");
        // ------------------------------
        LongPress = true;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        isPointDown = false;
        Debug.Log("鼠标抬起");
        ExitButton = false;
        LongPress = false;

        /*---------------------------------------------------------------*/
        OnceTime = true;
        T = null;
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        isPointDown = false;
        Debug.Log("鼠标退出");
        //-------------------------
        ExitButton = true;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        isPointDown = false;
        Debug.Log("鼠标点击");
    }

    public void ReGameObject(string Tag)
    {
        GameObject[] Object = GameObject.FindGameObjectsWithTag(Tag);
        foreach (GameObject Obj in Object)
        {
            Destroy(Obj);
        }
        value = 0;
        Button.interactable = true;
    }
}