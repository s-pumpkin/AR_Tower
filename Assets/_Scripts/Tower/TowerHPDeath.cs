using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerHPDeath : MonoBehaviour
{

    public enum data { 主堡, 攻擊塔, 資源塔, 護盾塔 };
    public data _data;

    public Image 血量條;
    public int Hp;
    private float HpMax;
    public float Hp百分比;
    private int Energy;
    private bool isDeath = false;
    public GameObject EneergyEffectBall;
    private ForceShieldController ForceShieldController;
    private AttTower AttTower;

    [Header("資源塔")]
    private MaterialTower MaterialTower;

    [Header("護盾塔")]
    private EnergyTower EnergyTower;

    [Header("通用")]
    public GameObject 塔死亡特效;

    // Start is called before the first frame update
    void Start()
    {
        switch (_data)
        {
            case data.攻擊塔:
                AttTower = gameObject.GetComponent<AttTower>();
                break;
            case data.資源塔:
                MaterialTower = gameObject.GetComponent<MaterialTower>();
                break;
            case data.護盾塔:
                EnergyTower = gameObject.GetComponent<EnergyTower>();
                break;
        }
        Hp = gameObject.GetComponent<DataSetting>().生命值;
        HpMax = Hp;
        ForceShieldController = EneergyEffectBall.GetComponent<ForceShieldController>();
    }

    public void Update()
    {
        if (PlayerSkill.instance != null)
        {
            ReEnergy(PlayerSkill.instance.Energy);
        }

        EneergyEffect();
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

    //護盾回復
    public void ReEnergy(int value)
    {
        if (PlayerSkill.instance.ToweruseSkill)
        {
            Energy = value;
        }
    }


    //受傷
    public void isHurt(int attack)
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
                TowerDeath();
                return;
            }
        }
    }

    public void EneergyEffect()
    {
        if (Energy > 0)
        {
            if (ForceShieldController._DissolveValue == 1)
            {
                ForceShieldController.PlayAppearingAnimation();
            }
            return;
        }

        if (Energy <= 0)
        {
            if (ForceShieldController._DissolveValue == 0)
            {
                ForceShieldController.PlayDisappearingAnimation();
            }
            return;
        }
    }

    void TowerDeath()
    {
        switch (_data)
        {
            case data.攻擊塔:
                AttTower.enabled = false;
                break;
            case data.資源塔:
                MaterialTower.enabled = false;
                break;
            case data.護盾塔:
                EnergyTower.enabled = false;
                break;
            case data.主堡:
                GameWave.instance.BadEndGame();
                break;
        }
        GameObject e = Instantiate(塔死亡特效, transform.position, transform.rotation);
        Destroy(e, 2f);

        Destroy(gameObject);

    }
}
