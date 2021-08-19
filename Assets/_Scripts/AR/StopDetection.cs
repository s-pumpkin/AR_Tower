using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class StopDetection : MonoBehaviour
{
    private static StopDetection _instance;
    public static StopDetection instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<StopDetection>();
            }
            return _instance;
        }
    }
    public void Stop()
    {
        GetComponent<ARPlaneManager>().enabled = false;
    }

    public void ReStart()
    {
      GetComponent<ARPlaneManager>().enabled = true;
    }


}
