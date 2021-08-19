using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCalculation : MonoBehaviour
{
    private static DistanceCalculation _instance;
    public static DistanceCalculation instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DistanceCalculation>();
            }
            return _instance;
        }
    }

    //塔用
    public GameObject TowerAttFish(Transform TowerPos)
    {
        //收尋場上所有Tag為敵人
        GameObject[] Fishs = GameObject.FindGameObjectsWithTag("敵人");
        //Debug.Log(Fishs.Length);
        //如果 Fishs 為空輸出null
        if (Fishs.Length == 0) return null;
        GameObject 最近者 = null;
        float 最近者距離 = 99999;
        foreach (var a in Fishs)
        {
            float distance = Vector3.Distance(TowerPos.position, a.transform.position); //計算兩者的距離
            // Debug.Log("現在計算距離====" + a.name + "，" + distance);
            if (最近者 != null)
            {
                if (distance < 最近者距離)  //判斷誰距離較近
                {
                    最近者 = a;
                    最近者距離 = distance;
                    // Debug.Log("最後的最近者" + a.name + "，" + distance);
                }
            }
            else
            {
                最近者 = a;
                最近者距離 = distance;
                // Debug.Log("缺少補充最近者====" + a.name + "，" + 最近者距離);
            }
        }
        // Debug.Log(最近者.name);
        return 最近者;
    }

    //敵人用
    public GameObject FishAttTower(Transform EnemyPos)
    {
        GameObject[] Towers = GameObject.FindGameObjectsWithTag("Tower");
        GameObject 最近者 = null;
        float 最近者距離 = 99999;
        foreach (var a in Towers)
        {
            //檢查塔是否生成
            if (a.GetComponent<DataSetting>().是否生成 == false)
            {
                continue; //跳過至下一個迴圈
            }
            // Debug.Log(a.name);
            float distance = Vector3.Distance(EnemyPos.position, a.transform.position); //計算兩者的距離
            // Debug.Log("現在計算距離====" + a.name + "，" + distance);
            if (最近者 != null)
            {
                if (distance < 最近者距離)  //判斷誰距離較近
                {
                    最近者 = a;
                    最近者距離 = distance;
                    // Debug.Log("最後的最近者" + a.name + "，" + distance);
                }
            }
            else
            {
                最近者 = a;
                最近者距離 = distance;
                // Debug.Log("缺少補充最近者====" + a.name + "，" + 最近者距離);
            }
        }
        // Debug.Log(最近者.name);
        return 最近者;
    }
}
