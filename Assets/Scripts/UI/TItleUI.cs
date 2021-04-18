using UnityEngine;

public class TItleUI : MonoBehaviour
{
    //スタートボタンを押す
　public void PushStartBtn()
    {
        SceneLoader.instance.LoadScene(SceneLoader.sceneName.Main);
    }
}
