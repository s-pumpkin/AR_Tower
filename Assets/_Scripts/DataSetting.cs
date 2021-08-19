using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSetting : MonoBehaviour
{
    public bool 是否生成 = false;
    public enum 類型 { 塔, 敵人 };
    public 類型 _類型;
    public int 生命值;
    [Tooltip("限定護盾塔")]
    public int 額外護盾值;
    public int 攻擊力;

    public float 攻擊距離;
    [Tooltip("限定攻擊塔或護盾塔")]
    public GameObject 偵測範圍_塔;
    [Tooltip("數字越小越快 & 功能塔的CD倒數")]
    public float 攻擊速度;


    [Tooltip("敵軍的移動速度")]
    public float 移動速度;

    [System.Serializable]
    public struct 生產
    {
        public int 資源;
        public int 電力;
        public int 零件;
    }
    public 生產 _生產;

    [System.Serializable]
    public struct 建造消費
    {
        public int 資源;
        public int 電力;
        public int 零件;
    }
    public 建造消費 _建造消費;

    [Header("Debug Display")]
    public Color attackRangeColor = Color.red;

    private void Start()
    {
        switch (_類型)
        {
            case 類型.塔:
                顯示攻擊距離Outline();
                break;
            case 類型.敵人:

                break;

        }

    }

    private void Update()
    {

    }

    void 顯示攻擊距離Outline()
    {
        if (偵測範圍_塔 != null)
        {
            偵測範圍_塔.transform.localScale = new Vector3(攻擊距離 * 2f, 偵測範圍_塔.transform.localScale.y, 攻擊距離 * 2f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Attack range
        Gizmos.color = attackRangeColor;
        Gizmos.DrawWireSphere(transform.position, 攻擊距離);
    }

}
