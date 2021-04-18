using UnityEngine;

public class ResultUI : MonoBehaviour
{
    //タイトルへ戻る
    public void PushBackTitleBtn()
    {
        SceneLoader.instance.LoadScene(SceneLoader.sceneName.Title);
    }

    //もう一度ゲームをプレイする
    public void PushRetryGameBtn()
    {
        SceneLoader.instance.LoadScene(SceneLoader.sceneName.Main);
    }
}
