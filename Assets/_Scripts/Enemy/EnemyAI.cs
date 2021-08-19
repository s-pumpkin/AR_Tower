using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum 狀態 { isIdeal, isRun };
    private 狀態 status = 狀態.isRun;
    public bool isDeath = false;

    private NavMeshAgent AI;
    [Tooltip("與塔的移動過去停止的距離")]
    public float safeDistance;

    private DataSetting DS;

    private Transform myTr;
    public GameObject Tower;
    public float Distance = 999;

    public bool Fire = false;
    public GameObject 發射點;
    private Transform BulletTr;
    public GameObject 子彈;
    public Vector3 調整擊中高度;
    private GameObject 子彈回收區;
    private float AttSpeedCD;
    private float timeCount = 0.5f;



    [Header("Debug Display")]
    public Color safeDistanceRangeColor = Color.blue;

    // Start is called before the first frame update
    void Start()
    {
        AI = gameObject.GetComponent<NavMeshAgent>();
        DS = gameObject.GetComponent<DataSetting>();
        isDeath = gameObject.GetComponent<EnemyHpDeath>().isDeath;
        子彈回收區 = GameObject.Find("子彈回收區");
        基礎設定();
        StartCoroutine(ReDistance());
    }

    void 基礎設定()
    {
        AI.speed = DS.移動速度;
        AI.stoppingDistance = safeDistance;
        AttSpeedCD = DS.攻擊速度;
        myTr = gameObject.transform;
        BulletTr = 發射點.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameWave.instance.stopEnemy)
        {
            行為模式();
            attackCD();
        }
    }

    //刷新與目標的距離
    IEnumerator ReDistance()
    {
        while (!isDeath)
        {
            if (Tower != null)
            {
                Distance = Vector3.Distance(myTr.position, Tower.transform.position);  //看距離多少
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    void 行為模式()
    {
        Tower = DistanceCalculation.instance.FishAttTower(myTr); //回傳最近目標
        if (Tower == null || isDeath)
        {
            Debug.Log("等待");
            status = 狀態.isIdeal;
            AI.isStopped = true;
            //RunToTower(gameObject.transform.position);
            return;
        }

        if (Tower != null)
        {
            AI.isStopped = false;
            RunToTower(Tower.transform.position);
            if (Distance > DS.攻擊距離)
            {
                status = 狀態.isRun;
                return;
            }

            if (Distance < DS.攻擊距離)
            {
                status = 狀態.isRun;
                if (Fire)
                {
                    Debug.Log(gameObject.name + "Fire");
                    Att();
                }
            }
        }
    }

    //移動至目標
    private void RunToTower(Vector3 pos)
    {
        AI.SetDestination(pos);
    }

    //攻擊CD
    public void attackCD()
    {
        timeCount -= Time.deltaTime;
        if (timeCount <= 0)
        {
            Fire = true;
            timeCount = AttSpeedCD;
        }
    }

    private void Att()
    {
        Fire = false;
        Quaternion lookEnemyRot = Quaternion.LookRotation(Tower.transform.position - BulletTr.position + 調整擊中高度); //子彈朝向的方向
        GameObject Bullet = Instantiate(子彈, BulletTr.position, lookEnemyRot, 子彈回收區.transform);
        Bullet.GetComponent<EnemyBullet>().attack = DS.攻擊力;
    }

    //顯示停留距離(遊戲中看不到)
    private void OnDrawGizmosSelected()
    {
        // Attack range
        Gizmos.color = safeDistanceRangeColor;
        Gizmos.DrawWireSphere(transform.position, safeDistance);
    }

}
