using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 測試用怪物生成 : MonoBehaviour
{
    public GameObject 怪物;
    public GameObject 位置;

    // Update is called once per frame
    void Update()
    {

    }

    public void 生成()
    {
        Instantiate(怪物, 位置.transform.position, 位置.transform.rotation);
    }
}
