using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NegativeEffect : MonoBehaviour
{
    //Singleton(單例模式)
    private static NegativeEffect _instance;
    public static NegativeEffect instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<NegativeEffect>();
            }
            return _instance;
        }
    }

    [Header("塔的攻擊速度狀態")]
    public bool NFTowerAttCD = false;
    public bool BFTowerAttCD = false;
    [System.NonSerialized]
    public float BNFTowerAttValue = 1;

    [Tooltip("怪物血量倍率")]
    public float MonsterHpMagnification = 1;

    // Start is called before the first frame update
    void Start()
    {
        NFTowerAttCD = false;
        BFTowerAttCD = false;
        InvokeRepeating("TowerAttCD", 0, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void TowerAttCD()
    {
        if ((NFTowerAttCD == true && BFTowerAttCD == true) || (NFTowerAttCD == false && BFTowerAttCD == false))
        {
            BNFTowerAttValue = 1;
            return;
        }

        if (NFTowerAttCD == true && BFTowerAttCD == false)
        {
            BNFTowerAttValue = 0.75f;
            return;
        }

        if (NFTowerAttCD == false && BFTowerAttCD == true)
        {
            BNFTowerAttValue = 1.25f;
            return;
        }
    }






}
