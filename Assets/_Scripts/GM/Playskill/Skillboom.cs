using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skillboom : MonoBehaviour
{

    public GameObject 核彈特效;
    public AudioSource _AudioSource;

    public int Speed;
    
    [HideInInspector]
    public int 傷害 = 100;
    void Update()
    {
        transform.Translate(0, Speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)  //底下要改
    {
        if (other.CompareTag("Ground"))
        {
            this.GetComponent<CapsuleCollider>().enabled = false;
            GameObject effect = Instantiate(核彈特效, other.transform.position, other.transform.rotation);
            Destroy(effect, 5.0f);
            if (_AudioSource != null && _AudioSource.clip != null)
            {
                _AudioSource.PlayOneShot(MainAudio.instance.AudioClass.爆炸音效);
            }
            GameObject[] enemy = GameObject.FindGameObjectsWithTag("敵人");
            foreach (var E in enemy)
            {
                EnemyHpDeath EnemyHp = E.gameObject.GetComponent<EnemyHpDeath>();
                EnemyHp.isHurt(傷害);
            }
            Destroy(gameObject);
        }
    }


}
