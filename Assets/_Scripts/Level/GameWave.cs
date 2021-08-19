using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 生怪
/// 遊戲結束
/// </summary>
/// 

public class GameWave : MonoBehaviour
{
    private static GameWave _instance;
    public static GameWave instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameWave>();
            }
            return _instance;
        }
    }
    private GameObject Plane;

    public bool stopEnemy = false; //暫停怪物
    public int 塔生成上限;

    [HideInInspector]
    public int 目前塔數量;
    public bool 鎖定塔生成 = false;

    public bool Material開始 = false;
    [Tooltip("怪物重生點 勿更改")]
    public GameObject[] MonsterRebirthValue;
    [Tooltip("生怪間隔")]
    public float MonsterRetime = 1.0f;

    public GameObject[] Monster;
    public GameObject Boss;

    public int TotalMonster;
    public int TotalBoss;

    public AudioSource _AudioSource;

    public GameObject 獲勝UI;
    public GameObject 煙火特效;
    public GameObject 失敗UI;
    bool GoodEndOnce = false;
    public void Start()
    {
        TotalMonster = MonsterValue.小怪數量;
        TotalBoss = MonsterValue.Boss數量;
    }

    private void Update()
    {
        GoodEndGame();

        //控制塔生成上限
        目前塔數量 = GameObject.FindGameObjectsWithTag("Tower").Length;
        if (目前塔數量 >= 塔生成上限)
        {
            鎖定塔生成 = true;
        }
        else
        {
            鎖定塔生成 = false;
        }
    }

    public void 刷新怪物重生點()
    {
        Plane = GameObject.FindGameObjectWithTag("Ground");
        MonsterRebirthValue = GameObject.FindGameObjectsWithTag("MonsterRebirth");
    }

    public void 開始生怪()
    {
        InvokeRepeating("生成", 0f, MonsterRetime);
        Material開始 = true;
        if (_AudioSource != null && _AudioSource.clip == null)
        {
            _AudioSource.PlayOneShot(MainAudio.instance.AudioClass.遊戲開始音效);
        }
    }

    void 生成()
    {
        Transform Rebirth = MonsterRebirthValue[Random.Range(0, MonsterRebirthValue.Length)].transform;
        if (TotalMonster > 0)
        {
            GameObject isMonster = Monster[Random.Range(0, Monster.Length)];
            GameObject T = Instantiate(isMonster, Rebirth.position, Rebirth.rotation);
            T.gameObject.transform.parent = Plane.gameObject.transform;
            TotalMonster -= 1;
            return;
        }
        if (TotalMonster == 0 && TotalBoss > 0)
        {
            GameObject T = Instantiate(Boss, Rebirth.position, Rebirth.rotation, Plane.transform);
            T.gameObject.transform.parent = Plane.gameObject.transform;
            TotalBoss -= 1;
            return;
        }
    }


    void GoodEndGame()
    {
        if (TotalMonster == 0 && TotalBoss == 0 && GameObject.FindGameObjectWithTag("敵人") == null && !GoodEndOnce)
        {
            GoodEndOnce = true;
            stopEnemy = true;
            GameUISetting.instance.GameUI.SetActive(false);
            GameObject a = GameObject.FindGameObjectWithTag("煙火放置位置");
            Instantiate(煙火特效, transform.position, transform.rotation);
            if (_AudioSource != null && _AudioSource.clip == null)
            {
                _AudioSource.PlayOneShot(MainAudio.instance.AudioClass.勝利);
            }
            Invoke("GoodEndGameUI", 1.5f);
        }
    }

    public void GoodEndGameUI()
    {
        獲勝UI.SetActive(true);
    }

    public void BadEndGame()
    {
        stopEnemy = true;
        失敗UI.SetActive(true);
    }
}
