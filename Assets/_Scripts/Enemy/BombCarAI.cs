using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BombCarAI : MonoBehaviour
{
    public AudioSource _AudioSource;
    private DataSetting DS;
    private GameObject 子彈回收區;

    private Transform myTr;
    private NavMeshAgent AI;
    private GameObject Tower;
    public GameObject BoomEffect;

    public float Distance;
    private bool isBoom = false;

    void Start()
    {
        DS = gameObject.GetComponent<DataSetting>(); //基本能力值
        AI = gameObject.GetComponent<NavMeshAgent>();
        基礎設定();
        myTr = gameObject.transform;
        子彈回收區 = GameObject.Find("子彈回收區");
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameWave.instance.stopEnemy)
        {
            搜尋移動至目標();
        }
    }

    void 基礎設定()
    {
        AI.speed = DS.移動速度;
    }

    public void 搜尋移動至目標()
    {
        if (!isBoom)
        {
            Tower = DistanceCalculation.instance.FishAttTower(myTr); //回傳最近目標
            if (Tower == null)
            {
                AI.SetDestination(gameObject.transform.position);
                return;
            }
            if ((Tower != null))
            {
                AI.SetDestination(Tower.transform.position);
                Distance = Vector3.Distance(myTr.position, Tower.transform.position);  //看距離多少
                return;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Tower")
        {
            return;
        }
        else if (!isBoom)
        {
            isBoom = true;
            //爆炸特效&&音效
            GameObject E = Instantiate(BoomEffect, transform.position, transform.rotation);
            if (_AudioSource.clip == null && _AudioSource != null)
            {
                _AudioSource.PlayOneShot(MainAudio.instance.AudioClass.爆炸音效);
            }
            Destroy(E, 1f);
            other.GetComponent<TowerHPDeath>().isHurt(DS.攻擊力);
            Destroy(gameObject);
        }
    }




}
