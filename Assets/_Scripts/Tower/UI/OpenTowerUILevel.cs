using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTowerUILevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenLevelUI(GameObject TowerLevelUI)
    {
        GameObject[] UI = GameObject.FindGameObjectsWithTag("LevelUI");
        foreach (var a in UI)
        {
            if (a.activeSelf != false)
            {
                a.SetActive(false);
                //a.transform.parent.gameObject.transform.Find("範圍outLine").gameObject.SetActive(false);
            }
        }

        TowerLevelUI.SetActive(true);
        //TowerLevelUI.transform.parent.gameObject.transform.Find("範圍outLine").gameObject.SetActive(true);
    }

    public void ClosslUI()
    {
        GameObject[] UI = GameObject.FindGameObjectsWithTag("LevelUI");
        foreach (var a in UI)
        {
            if (a.activeSelf != false)
            {
                a.SetActive(false);
                //a.transform.parent.gameObject.transform.Find("範圍outLine").gameObject.SetActive(false);
            }
        }
    }


}
