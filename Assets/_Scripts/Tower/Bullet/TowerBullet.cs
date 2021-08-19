using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBullet : MonoBehaviour
{
    public enum _子彈類型 { 單發, 範圍 };
    public _子彈類型 子彈類型 = _子彈類型.單發;

    public bool isAttack = true;

    [Header("單發")]
    [Tooltip("拋物線Speed請設定0，並添加parabola.cs")]
    public int Speed;
    [Header("範圍")]
    public float 爆炸範圍大小;
    public GameObject BoomEffect;
    public float 特效存活時間 = 1f;
    public Color 爆炸範圍大小Color = Color.red;
    [Header("通用")]
    //[System.NonSerialized]
    public int attack = 20;
    public float 子彈存活時間 = 5f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, Speed * Time.deltaTime);
        Destroy(gameObject, 子彈存活時間);
    }

    private void OnTriggerEnter(Collider other)  //底下要改
    {
        if (isAttack)
        {
            if (子彈類型 == _子彈類型.單發)
            {
                if (other.CompareTag("敵人"))
                {
                    Debug.Log("子彈名稱==" + gameObject.name + "、被打到名稱==" + other.name);
                    other.GetComponent<EnemyHpDeath>().isHurt(attack);
                    Destroy(gameObject);
                }
                return;
            }

            if (子彈類型 == _子彈類型.範圍)
            {
                Debug.Log("子彈名稱==" + gameObject.name + "、被打到名稱==" + other.name);

                Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, 爆炸範圍大小);
                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag("敵人"))
                    {
                        Debug.Log("子彈名稱==" + gameObject.name + "、被打到名稱==" + collider.name);
                        EnemyHpDeath EnemyHpDeath = collider.transform.GetComponent<EnemyHpDeath>();
                        EnemyHpDeath.isHurt(attack);
                    }
                }
                if (BoomEffect != null && other.CompareTag("敵人"))
                {
                    GameObject _BoomEffect = (GameObject)Instantiate(BoomEffect, other.transform.position, other.transform.rotation);
                    Destroy(_BoomEffect, 特效存活時間);
                    Destroy(gameObject);
                }
                
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        // Attack range
        Gizmos.color = 爆炸範圍大小Color;
        Gizmos.DrawWireSphere(transform.position, 爆炸範圍大小);
    }
}
