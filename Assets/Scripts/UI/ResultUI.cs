using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [SerializeField]
 Text   resultScoreText; //リザルトスコアを表示するテキスト

    private void Start()
    {
        //結果スコアをテキストに表示
        resultScoreText.text = ScoreManager.instance.MyScore.ToString();
    }

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
