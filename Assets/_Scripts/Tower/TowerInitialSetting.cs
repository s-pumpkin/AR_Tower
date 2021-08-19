using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInitialSetting : MonoBehaviour
{
    ///<summary>
    ///塔的初始設定值
    ///</summary>

    //一階塔
    [System.Serializable]
    public struct MachineTowerLevell
    {
        public int HpMax;
        public int Attack;
        public float Attack_Speed;
        public float Attack_Distance;
    }

    [System.Serializable]
    public struct LaserTowerLevell
    {
        public int HpMax;
        public int Attack;
        public float Attack_speed;
        public float Attack_Distance;
    }

    //二階塔
    [System.Serializable]
    public struct 加農砲
    {
        public int HpMax;
        public int Attack;
        public float Attack_Speed;
        public float Attack_Distance;
    }

    [System.Serializable]
    public struct 機關槍
    {
        public int HpMax;
        public int Attack;
        public float Attack_speed;
        public float Attack_Distance;
    }

    [System.Serializable]
    public struct 巡弋飛彈
    {
        public int HpMax;
        public int Attack;
        public float Attack_speed;
        public float Attack_Distance;
    }

    [System.Serializable]
    public struct 激光槍
    {
        public int HpMax;
        public int Attack;
        public float Attack_speed;
        public float Attack_Distance;
    }

    [System.Serializable]
    public struct 電漿砲
    {
        public int HpMax;
        public int Attack;
        public float Attack_speed;
        public float Attack_Distance;
    }

    [System.Serializable]
    public struct 電磁軌道砲
    {
        public int HpMax;
        public int Attack;
        public float Attack_speed;
        public float Attack_Distance;
    }

    [System.Serializable]
    public struct 護頓塔
    {
        public int HpMax;
        public int EnergyShield; //能量護頓值
        public float Skill_Distance;
    }

    [System.Serializable]
    public struct 金幣塔
    {
        public int HpMax;
        public int ProductVal; //能量護頓值
    }

    [System.Serializable]
    public struct 電能塔
    {
        public int HpMax;
        public int ProductVal; //能量護頓值
    }

    [System.Serializable]
    public struct 兵工廠
    {
        public int HpMax;
        public int ProductVal; //能量護頓值
    }

    [HeaderAttribute("一階塔攻擊塔")]
    public MachineTowerLevell _MachineTowerLevell;
    public LaserTowerLevell _LaserTowerLevell;

    [HeaderAttribute("二階塔攻擊塔")]
    public 加農砲 _加農砲;
    public 機關槍 _機關槍;
    public 巡弋飛彈 _巡弋飛彈;
    public 激光槍 _激光槍;
    public 電漿砲 _電漿砲;
    public 電磁軌道砲 _電磁軌道砲;

    [HeaderAttribute("支援塔")]
    public 護頓塔 _護頓塔;
    public 金幣塔 _金幣塔;
    public 電能塔 _電能塔;
    public 兵工廠 _兵工廠;


}
