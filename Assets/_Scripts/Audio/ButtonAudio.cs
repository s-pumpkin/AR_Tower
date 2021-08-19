using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAudio : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource _AudioSourceButton;
    public Sprite audioOpen;
    public Sprite audioClose;

    public void StartButtonAudio()
    {
        if (_AudioSourceButton.clip == null && _AudioSourceButton != null)
        {
            _AudioSourceButton.PlayOneShot(MainAudio.instance.AudioClass.按鈕音效);
        }
    }

    public void AudioLOGO(GameObject solf)
    {
        Debug.Log("音樂關閉開啟");
        MainAudio.instance.Muisce();
        if (MainAudio.instance.MasterAuidoBool)
        {
            solf.GetComponent<Image>().sprite = audioOpen;
            return;
        }
        if (!MainAudio.instance.MasterAuidoBool)
        {
            solf.GetComponent<Image>().sprite = audioClose;
            return;
        }

    }
}
