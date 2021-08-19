using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDeletr : MonoBehaviour
{
    public GameObject 自己Tower;
    public GameObject 移除特效;
    public float 特效時間;

    public void 移除()
    {
        if (移除特效 != null)
        {
            GameObject effect = Instantiate(移除特效, 自己Tower.transform.position, 自己Tower.transform.rotation);
            Destroy(effect, 特效時間);
        }
        Destroy(自己Tower);
    }
}
