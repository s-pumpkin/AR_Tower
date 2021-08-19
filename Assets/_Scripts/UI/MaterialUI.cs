using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialUI : MonoBehaviour
{
    private static MaterialUI _instance;
    public static MaterialUI instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MaterialUI>();
            }
            return _instance;
        }
    }

    //總共多少
    public int 總資源 = 0;
    public int 總發電 = 0;
    public int 總零件 = 0;

    public Text 資源text;
    public Text 發電text;
    public Text 零件text;

    // Update is called once per frame
    void Update()
    {
        if (資源text == null || 發電text == null || 零件text == null)
        {
            Debug.LogError("GM/MaterialUI裝置沒放");
            return;
        }
        資源text.text = 總資源.ToString();
        發電text.text = 總發電.ToString();
        零件text.text = 總零件.ToString();
    }

    public void UseMaterial(int 資源, int 發電, int 零件)
    {
        總資源 -= 資源;
        總發電 -= 發電;
        總零件 -= 零件;
    }
}
