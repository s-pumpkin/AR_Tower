using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyTower : MonoBehaviour
{
    public bool isEnergy = true;
    private DataSetting DS;


    void Start()
    {
        DS = gameObject.GetComponent<DataSetting>();
    }

    // Update is called once per frame
    void Update()
    {
        if(DS.是否生成)
        {
            Energy();
        }
    }

    public void Energy()
    {
        if (isEnergy)
        {
            GameObject[] Tower = GameObject.FindGameObjectsWithTag("Tower");
            foreach (var TowerHPDeath in Tower)
            {
                float distance = Vector3.Distance(TowerHPDeath.transform.position, transform.position);
                if (distance <= DS.攻擊距離)
                {
                    TowerHPDeath HPEnergy = TowerHPDeath.GetComponent<TowerHPDeath>();
                    HPEnergy.ReEnergy(DS.額外護盾值);
                }
            }
            isEnergy = false;
        }

    }
}
