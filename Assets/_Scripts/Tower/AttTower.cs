using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttTower : MonoBehaviour
{
    public enum _砲塔類型 { 一二階砲台單射, 二階雷射蓄力, 二階砲台拋物線 };
    public _砲塔類型 砲塔類型 = _砲塔類型.一二階砲台單射;

    private Transform myTr;
    public GameObject enemy;
    private EnemyHpDeath EnemyHpDeath;

    public Transform gunTr;
    public float 旋轉速度 = 10f;

    public Transform 發射點;
    private GameObject 子彈回收區;

    private DataSetting DS;
    private float timeCount;
    public AudioSource _AudioSource;

    public bool Fire = true;

    [Header("Tower")]
    public GameObject 子彈;
    public GameObject TowerLightEffect;
    public float TowerLightEffectTimeCount;

    [Header("Laser")]
    public GameObject LaserAccumulateEffect;
    public Vector3 LaserAccumulateEffectScale;
    public float ChangeSpeed;
    public LineRenderer lineRenderer;
    public Vector3 調整雷射終點位置;
    public ParticleSystem impactEffect;
    //public Light impactLight;

    void Start()
    {
        DS = gameObject.GetComponent<DataSetting>(); //基本能力值
        子彈回收區 = GameObject.Find("子彈回收區");
        myTr = gameObject.transform;
        InvokeRepeating("UpdateTowar", 0f, 0.5f);

        if (砲塔類型 == _砲塔類型.二階雷射蓄力)
        {
            set();
        }

    }

    private void Update()
    {
        if (DS.是否生成)
        {
            TowardsTheTarget();
            attCD();
        }
    }

    public void set()
    {
        if (砲塔類型 == _砲塔類型.二階雷射蓄力)
        {
            if (lineRenderer.enabled)
            {
                LaserAccumulateEffect.SetActive(false);
                LaserAccumulateEffect.transform.localScale = new Vector3(0, 0, 0);
                lineRenderer.enabled = false;
                impactEffect.Stop();
                //impactLight.enabled = false;
            }
        }

    }

    public void UpdateTowar()
    {
        enemy = DistanceCalculation.instance.TowerAttFish(myTr); //回傳最近目標
        //尋找最近的敵人，目標死亡後在尋找下一個
        if (enemy == null)
        {
            set();
        }
    }

    //有敵人
    public void TowardsTheTarget()
    {
        if (enemy != null)
        {
            Vector3 vec = enemy.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(vec);
            Vector3 rotation = Quaternion.Lerp(gunTr.rotation, lookRotation, Time.deltaTime * 旋轉速度).eulerAngles;
            gunTr.rotation = Quaternion.Euler(0f, rotation.y, 0f);


            float Distance = Vector3.Distance(new Vector3(myTr.position.x, 0, myTr.position.z), new Vector3(enemy.transform.position.x, 0, enemy.transform.position.z));
            if(Distance > DS.攻擊距離)
            {
                 set();
            }
            if (Fire && Distance <= DS.攻擊距離)
            {
                EnemyHpDeath = enemy.GetComponent<EnemyHpDeath>();
                switch (砲塔類型)
                {
                    case _砲塔類型.一二階砲台單射:
                        Level1Tower();
                        break;
                    case _砲塔類型.二階雷射蓄力:
                        Level2Tower();
                        break;
                    case _砲塔類型.二階砲台拋物線:
                        Level3Tower();
                        break;
                }
            }
        }
    }


    //模式
    public void Level1Tower()
    {
        if (TowerLightEffect != null)
        {
            GameObject effect = Instantiate(TowerLightEffect, 發射點);
            Destroy(effect, TowerLightEffectTimeCount);
        }
        Att();
    }


    public void Level2Tower()
    {
        if (LaserAccumulateEffect != null)
        {
            if (LaserAccumulateEffect.activeSelf != true)
            {
                LaserAccumulateEffect.SetActive(true);
            }
            if (LaserAccumulateEffect.transform.localScale != LaserAccumulateEffectScale)
            {
                LaserAccumulateEffect.transform.localScale = Vector3.MoveTowards(LaserAccumulateEffect.transform.localScale, LaserAccumulateEffectScale, ChangeSpeed * Time.deltaTime);
                return;
            }
        }
        Att2();
    }

    public void Level3Tower()
    {
        if (TowerLightEffect != null)
        {
            GameObject effect = Instantiate(TowerLightEffect, 發射點);
            Destroy(effect, TowerLightEffectTimeCount);
        }
        Att3();
    }

    //攻擊敵人
    public void attCD()
    {
        timeCount -= Time.deltaTime;
        Debug.Log(Fire);
        if (timeCount <= 0)
        {
            Fire = true;
            timeCount = DS.攻擊速度 * NegativeEffect.instance.BNFTowerAttValue;
        }
    }


    public void Att()
    {
        Fire = false;
        Debug.Log("Fire");
        Quaternion lookEnemyRot = Quaternion.LookRotation(enemy.transform.position - 發射點.position); //子彈朝向的方向
        GameObject Bullet = Instantiate(子彈, 發射點.position, lookEnemyRot, 子彈回收區.transform);
        Bullet.GetComponent<TowerBullet>().attack = DS.攻擊力;
        //音效
        if (_AudioSource != null && _AudioSource.clip == null)
        {
            _AudioSource.PlayOneShot(MainAudio.instance.AudioClass.攻擊音效);
        }
    }

    public void Att2()
    {
        EnemyHpDeath.isHurt(DS.攻擊力 * Time.deltaTime);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            //impactLight.enabled = true;
        }

        Vector3 enemyPos = enemy.transform.position + 調整雷射終點位置;
        lineRenderer.SetPosition(0, 發射點.position);
        lineRenderer.SetPosition(1, enemyPos);

        //音效
        if (_AudioSource != null && _AudioSource.clip == null)
        {
            _AudioSource.PlayOneShot(MainAudio.instance.AudioClass.雷射音效);
        }
        //擊中特效位子
        Vector3 dir = enemyPos - 發射點.position;
        impactEffect.transform.position = enemyPos;// / 3 * 2 * enemy.transform.localScale.x
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    public void Att3()
    {
        Fire = false;
        Debug.Log("Fire");
        Quaternion lookEnemyRot = Quaternion.LookRotation(enemy.transform.position - 發射點.position); //子彈朝向的方向
        GameObject Bullet = Instantiate(子彈, 發射點.position, lookEnemyRot, 子彈回收區.transform);
        Bullet.GetComponent<TowerBullet>().attack = DS.攻擊力;

        parabola BulletParabola = Bullet.GetComponent<parabola>();
        BulletParabola.pointA = 發射點;
        BulletParabola.pointB = enemy.transform;
        BulletParabola.StartBullet();
        BulletParabola.StartMove = true;
        //音效
        if (_AudioSource != null && _AudioSource.clip == null)
        {
            _AudioSource.PlayOneShot(MainAudio.instance.AudioClass.攻擊音效);
        }
    }
}
