using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MainAudio : MonoBehaviour
{
    private static MainAudio _instance;
    public static MainAudio instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MainAudio>();
            }
            return _instance;
        }
    }

    public AudioMixer audioMixer;
    public AudioClass AudioClass;



    public bool MasterAuidoBool = true;
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("AudioGM").Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Muisce()
    {
        MasterAuidoBool = !MasterAuidoBool;
        Value();
    }

    private void Value()
    {

        if (MasterAuidoBool)
        {
            audioMixer.SetFloat("Master", 0.0f);
            return;
        }

        if (!MasterAuidoBool)
        {
            audioMixer.SetFloat("Master", -80.0f);
            return;
        }
      

    }


}
