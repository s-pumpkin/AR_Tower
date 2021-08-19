using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // [System.NonSerialized]
    public int attack = 20;
    public int Speed;
    public float 子彈存活時間 = 5f;

    void Start()
    {

    }

    void Update()
    {
        transform.Translate(0, 0, Speed * Time.deltaTime);
        Destroy(gameObject, 子彈存活時間);
    }

    private void OnTriggerEnter(Collider other)  //底下要改
    {
        if (other.tag == "Tower")
        {
            other.GetComponent<TowerHPDeath>().isHurt(attack);
            Destroy(gameObject);
        }
    }
}