using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialTower : MonoBehaviour
{

    public enum data { 資源, 電力, 零件 };
    public data _data;
    private int matvalue;
    private float timeCount;
    private float isCD;
    private DataSetting DS;


    // Start is called before the first frame update
    void Start()
    {
        DS = gameObject.GetComponent<DataSetting>();
        timeCount = DS.攻擊速度 * NegativeEffect.instance.BNFTowerAttValue;
        switch (_data)
        {
            case data.資源:
                matvalue = DS._生產.資源;
                break;
            case data.電力:
                matvalue = DS._生產.電力;
                break;
            case data.零件:
                matvalue = DS._生產.零件;
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (DS.是否生成)
        {
            ReCD();
        }
    }

    //持續生成
    public void 生產資源(int value)
    {
        switch (_data)
        {
            case data.資源:
                MaterialUI.instance.總資源 += value;
                break;
            case data.電力:
                MaterialUI.instance.總發電 += value;
                break;
            case data.零件:
                MaterialUI.instance.總零件 += value;
                break;
        }
    }

    public void ReCD()
    {
        if(GameWave.instance.Material開始)
        {
            timeCount -= Time.deltaTime;
            if (timeCount <= 0)
            {
                生產資源(matvalue);
                timeCount = DS.攻擊速度 * NegativeEffect.instance.BNFTowerAttValue;
            }
        }
        
    }

}
