using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject objectToPlace;
    public GameObject placementIndicator;
    public GameObject 尋找水平面Text;
    public GameObject OKButton;
    private ARRaycastManager raycastManager;
    private Pose placementPose;
    private bool placementPoseIsValid = false;

    // public Outline PlaneOutline;
    public GameObject OpGameUI;
    bool SurePlanel = false;

    void Start()
    {
        raycastManager = this.gameObject.GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (SurePlanel != true)
        {
            UpdatePlacementPose();
            UpdatePlacementIndicator();
        }
        // Debug.Log("Plane:"+placementIndicator.activeSelf);
        // Debug.Log("Button:"+SurePlanel);
    }

    //是否偵測到水平後開啟平面物件
    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            尋找水平面Text.SetActive(false);
            placementIndicator.SetActive(true);
            OKButton.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
            OKButton.SetActive(false);
            尋找水平面Text.SetActive(true);
        }
    }

    //偵測是否水平平面
    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));//3改成2了 如果有問題
        var hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            var cameraForward = Camera.main.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

    public void 確認位置(GameObject button)
    {
        if (placementIndicator.activeSelf == true)
        {
            SurePlanel = true;
            button.SetActive(false);
            OpGameUI.SetActive(true);
            // PlaneOutline.enabled = false;
        }
    }

}
