using UnityEngine;
using UnityEngine.UI;

public class TItleUI : MonoBehaviour
{
    [SerializeField]
  Text  myHighScoreText; //ハイスコアを表示するテキスト
    [SerializeField]
    AudioSource mainAudioSource;
    [SerializeField]
    AudioClip PushButtonSound;

    private void Start()
    {
        //保存されているハイスコアの値を呼びだしてテキストに表示
        myHighScoreText.text = "YOUR HIGH SCORE:" + PrefasWrapper.GetSaveData(PrefasWrapper.KEY.Score).ToString();
    }

    //スタートボタンを押す
    public void PushStartBtn()
    {
        SceneLoader.instance.LoadScene(SceneLoader.sceneName.Main);
        mainAudioSource.PlayOneShot(PushButtonSound);
    }
}
