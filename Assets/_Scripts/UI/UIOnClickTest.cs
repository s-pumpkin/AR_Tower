using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;


public class UIOnClickTest : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerClickHandler
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
    public GameObject 生成Tower;
    public int 消耗資源;
    public GameObject T;

    private GameObject Plane;

    public AudioSource _AudioSource;
    void Start()
    {
        Plane = GameObject.FindGameObjectWithTag("Ground");
    }

    void Update()
    {
        UseMoney();
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
        CreateTower();
        //Debug.Log("UIOnClickTest--------------召喚:" + T);
    }

    void UseMoney()
    {
        if (GameWave.instance.鎖定塔生成)
        {
            Button.interactable = false;
            return;
        }

        if (MaterialUI.instance.總資源 < 消耗資源)
        {
            Button.interactable = false;
        }
        else
        {
            Button.interactable = true;
        }
    }

    //先按下 離開的同時是否長按 生成一個物件 放開後在手放開的位置生成物件 並把 手指按住的刪除
    void CreateTower()
    {
        if (LongPress && ExitButton && Button.interactable)    //生成塔
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);  //滑鼠測試可以
                                                                        //Ray r = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(r, out hit, Mathf.Infinity))
            {
                Debug.Log(hit);
                if (hit.transform.gameObject.tag == "Ground")
                {
                    if (OnceTime)
                    {
                        OnceTime = false;
                        T = (GameObject)Instantiate(生成Tower);
                        T.gameObject.transform.parent = Plane.gameObject.transform;
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
        if (OnceTime == false && T != null)
        {
            MaterialUI.instance.UseMaterial(消耗資源, 0, 0);
            DataSetting DataSet = T.GetComponent<DataSetting>();
            DataSet.是否生成 = true;
            if (_AudioSource.clip == null && _AudioSource != null)
            {
                _AudioSource.PlayOneShot(MainAudio.instance.AudioClass.放置音效);
            }
            if (DataSet.偵測範圍_塔 != null)
            {
                DataSet.偵測範圍_塔.SetActive(false);
            }
            OnceTime = true;
            T = null;
        }

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
}