using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TowerLevelUp : MonoBehaviour
{

    public enum 類型 { 砲彈塔, 雷射塔 };
    public 類型 _類型;

    public GameObject 自己Tower;
    public GameObject 升級Tower;
    public GameObject 升級特效;
    public float 特效時間;
    public AudioSource _AudioSource;


    [System.Serializable]
    public struct 建造消費
    {
        public int 資源;
        public int 電力;
        public int 零件;
    }
    public 建造消費 _建造消費;
    private MaterialUI MaterialUI;
    private Button 按鈕;

    // Start is called before the first frame update
    void Start()
    {
        按鈕 = gameObject.GetComponent<Button>();
        MaterialUI = GameObject.Find("GM").GetComponent<MaterialUI>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (_類型)
        {
            case 類型.砲彈塔:
                if (MaterialUI.總資源 > _建造消費.資源 && MaterialUI.總零件 > _建造消費.零件)
                {
                    按鈕.interactable = true;
                }
                else
                {
                    按鈕.interactable = false;
                }
                break;
            case 類型.雷射塔:
                if (MaterialUI.總發電 > _建造消費.電力 && MaterialUI.總資源 > _建造消費.資源)
                {
                    按鈕.interactable = true;
                }
                else
                {
                    按鈕.interactable = false;
                }
                break;

        }
    }

    public void 基礎LevelUP()
    {
        MaterialUI.instance.UseMaterial(_建造消費.資源, _建造消費.電力, _建造消費.零件);
        if (升級特效 != null)
        {
            GameObject effect = Instantiate(升級特效, 自己Tower.transform.position, 自己Tower.transform.rotation);
            Destroy(effect, 特效時間);
        }
        if (_AudioSource != null && _AudioSource.clip == null)
        {
            _AudioSource.PlayOneShot(MainAudio.instance.AudioClass.升級音效);
        }
        Instantiate(升級Tower, 自己Tower.transform.position, 自己Tower.transform.rotation);
        Destroy(自己Tower);
    }

    public void 雷射LevelUP()
    {
        MaterialUI.instance.UseMaterial(_建造消費.資源, _建造消費.電力, _建造消費.零件);
        if (升級特效 != null)
        {
            GameObject effect = Instantiate(升級特效, 自己Tower.transform.position, 自己Tower.transform.rotation);
            Destroy(effect, 特效時間);
        }
        if (_AudioSource != null && _AudioSource.clip == null)
        {
            _AudioSource.PlayOneShot(MainAudio.instance.AudioClass.升級音效);
        }
        Instantiate(升級Tower, 自己Tower.transform.position, 自己Tower.transform.rotation);
        Destroy(自己Tower);
    }


}
