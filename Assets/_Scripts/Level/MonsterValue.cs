using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterValue : MonoBehaviour
{

    public static int 小怪數量 = 25;
    public Text 小怪數量Text;
    public static int Boss數量 = 1;
    public Text Boss數量Text;

    void Start()
    {
        小怪數量Text.text = 小怪數量.ToString();
        Boss數量Text.text = Boss數量.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MonsterControl(int Value)
    {
        小怪數量 += Value;
        小怪數量 = Mathf.Clamp(小怪數量, 25, 999);
        小怪數量Text.text = 小怪數量.ToString();
    }

    public void BossControl(int Value)
    {
        Boss數量 += Value;
        Boss數量 = Mathf.Clamp(Boss數量, 1, 999);
        Boss數量Text.text = Boss數量.ToString();
    }
}
