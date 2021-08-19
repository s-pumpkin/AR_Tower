using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncOperationScene : MonoBehaviour
{
    AsyncOperation async;
    public string 場景名稱 = "";
    private void Start()
    {
        if (!string.IsNullOrEmpty(場景名稱))
        {
            async = SceneManager.LoadSceneAsync(場景名稱);
            //先禁止async執行動作，避免一執行專案就把場景轉換走
            async.allowSceneActivation = false;
        }
    }

    void Update()
    {
        //async.progress可以看場景的載入進度(0.0~1.0)，可透過此方法做進度條
        //如果只是仙芋仔好場景，還沒有要使用，進度就會停在0.9
        if (!string.IsNullOrEmpty(場景名稱))
        {
            Debug.Log(async.progress);
        }
    }

    public void 載入場景()
    {
        //需要轉換的時候，活化async，即可完成目的
        async.allowSceneActivation = true;
    }

    public void 起點()
    {

        SceneManager.LoadScene("Start");
    }

    public void 遊戲開始()
    {
        SceneManager.LoadScene("AR_Game");
    }

    public void 關閉遊戲()
    {
        Application.Quit();
    }
}
