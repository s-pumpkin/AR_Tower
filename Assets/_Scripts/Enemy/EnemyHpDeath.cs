using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpDeath : MonoBehaviour
{
    public Image 血量條;
    [HideInInspector]
    public float Hp;
    private float HpMax;
    public float Hp百分比;
    private float Energy;
    public bool isDeath = false;


    // Start is called before the first frame update
    void Start()
    {
        Hp = gameObject.GetComponent<DataSetting>().生命值;
        HpMax = Hp;
    }

    // Update is called once per frame
    void Update()
    {
        ShowHpValue();
    }

    private void ShowHpValue()
    {
        if (血量條 != null)
        {
            Hp百分比 = Hp / HpMax;
            血量條.fillAmount = Hp百分比;
        }
    }

    //受傷
    public void isHurt(float attack)
    {
        if (Energy > 0)
        {
            Energy -= attack;
            if (Energy < 0)
            {
                Hp += Energy;
            }
        }
        else
        {
            Hp -= attack;
            if (Hp <= 0)
            {
                isDeath = true;
                EnemyDeath();
                return;
            }
        }
    }

    void EnemyDeath()
    {
        Destroy(gameObject);
    }
}
