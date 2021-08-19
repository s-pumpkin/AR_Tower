using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TowerValue : MonoBehaviour
{
    public Text 最大;
    public Text 目前;

    // Start is called before the first frame update
    public void Update()
    {
        最大.text = GameWave.instance.塔生成上限.ToString();
        目前.text = GameWave.instance.目前塔數量.ToString();
    }
}
