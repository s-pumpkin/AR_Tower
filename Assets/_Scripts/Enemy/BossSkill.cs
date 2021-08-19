using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : MonoBehaviour
{
    private EnemyHpDeath HpDeathCs;
    float Hp;
    private BossAI BossAI;
    private DataSetting DS;

    public bool skill1 = false;
    [Tooltip("增加血量")]
    public int HpReValue;
    [Tooltip("增加攻擊力")]
    public int AddAttackValue;
    public bool skill2 = false;
    
    public bool skill3 = false;
    public GameObject 穿雲箭;
    // Start is called before the first frame update
    void Start()
    {
        HpDeathCs = gameObject.GetComponent<EnemyHpDeath>();
        BossAI = gameObject.GetComponent<BossAI>();
        DS = gameObject.GetComponent<DataSetting>();
        InvokeRepeating("BossSkilltime", 0f, 0.5f);
    }

    // Update is called once per frame
    public void BossSkilltime()
    {
        Hp = HpDeathCs.Hp百分比 * 100;
        //
        if (Hp <= 75 && !skill1)
        {
            skill1 = true;
            HpDeathCs.Hp += HpReValue;
            DS.攻擊力 += AddAttackValue;
            return;
        }

        //塔的Debuff減速
        if (Hp <= 50 && !skill2)
        {
            skill2 = true;
            NegativeEffect.instance.NFTowerAttCD = true;
            return;
        }

        //把子彈會成穿雲箭
        if (Hp <= 25 && !skill3)
        {
            skill3 = true;
            BossAI.子彈 = 穿雲箭;
            return;
        }
    }

    public void ReSkill()
    {
        NegativeEffect.instance.NFTowerAttCD = false;
    }
}
