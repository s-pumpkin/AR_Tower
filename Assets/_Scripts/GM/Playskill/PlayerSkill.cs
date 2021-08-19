using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    private static PlayerSkill _instance;
    public static PlayerSkill instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerSkill>();
            }
            return _instance;
        }
    }

    public AudioSource _AudioSource;

    [Header("護盾")]
    public bool useSkill;
    [HideInInspector]
    public bool ToweruseSkill = false;
    public Button Button1;
    public Text SkillCdtext;
    public int Energy = 100;
    public float ReTime = 60.0f;
    private float TimeCount = 60.0f;
    [Header("射擊")]
    public Button Button2;

    [Header("核彈")]
    public bool useSkill3;
    public Button Button3;
    public Text Skil3Cdtext;
    public GameObject 核彈;
    public int 攻擊傷害 = 100;
    public Vector3 降落高度;
    public float ReTime3 = 60.0f;
    private float TimeCount3 = 0.0f;
    public int 消耗電力 = 0;
    public int 消耗零件 = 0;
    public int 消耗資源 = 0;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LookMaterial();

        if (useSkill)
        {
            Skill1CD();
        }
        if (useSkill3)
        {
            Skill3CD();
        }
    }

    public void Skill1()
    {
        Button1.interactable = false;
        TimeCount = ReTime;
        useSkill = true;
        ToweruseSkill = true;
        Debug.Log(ToweruseSkill);
        if (_AudioSource != null && _AudioSource.clip == null)
        {
            _AudioSource.PlayOneShot(MainAudio.instance.AudioClass.玩家技能護頓音效);
        }
        Invoke("FalseToweruseSkill",1f);
    }

    void FalseToweruseSkill()
    {
        ToweruseSkill = false;
        Debug.Log(ToweruseSkill);

    }

    void Skill1CD()
    {
        TimeCount -= Time.deltaTime;
        SkillCdtext.text = ((int)TimeCount).ToString();

        if (TimeCount < 1)
        {
            useSkill = false;
            SkillCdtext.text = null;
            Button1.interactable = true;
        }

    }

    /*------------------------------------------------------------------------------------*/
    public void Skill2()
    {

    }

    /*------------------------------------------------------------------------------------*/
    public void Skill3()
    {
        Button3.interactable = false;
        TimeCount3 = ReTime3;
        useSkill3 = true;

        MaterialUI.instance.UseMaterial(消耗資源, 消耗電力, 消耗零件);

        GameObject[] Tower = GameObject.FindGameObjectsWithTag("敵人");
        Vector3 BoomPos = Tower[Random.Range(0, Tower.Length)].transform.localPosition + 降落高度;
        GameObject boom = Instantiate(核彈, BoomPos, Quaternion.Euler(0, 0, 0));
        boom.GetComponent<Skillboom>().傷害 = 攻擊傷害;
    }

    void Skill3CD()
    {
        TimeCount3 -= Time.deltaTime;
        Skil3Cdtext.text = ((int)TimeCount3).ToString();

        if (TimeCount3 < 1)
        {
            useSkill3 = false;
            Skil3Cdtext.text = null;
            Button1.interactable = true;
        }
    }

    void LookMaterial()
    {
        MaterialUI Materia = MaterialUI.instance;
        if (Materia.總資源 >= 消耗資源 && Materia.總發電 >= 消耗電力 && Materia.總零件 >= 消耗零件 && !useSkill3)
        {
            Button3.interactable = true;
        }
    }

}
