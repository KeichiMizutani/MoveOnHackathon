using UnityEngine;
using UnityEngine.UI;

public class TItleUI : MonoBehaviour
{
    [SerializeField]
  Text  myHighScoreText; //自分の所持スコア
    [SerializeField]
    AudioSource mainAudioSource;
    [SerializeField]
    AudioClip PushButtonSound;

    private void Start()
    {
        //ハイスコアを呼びだしてテキストに表示する
        myHighScoreText.text = "YOUR HIGH SCORE:" + PrefasWrapper.GetSaveData(PrefasWrapper.KEY.Score).ToString();
    }

    //�X�^�[�g�{�^�����

    public void PushStartBtn()
    {
        SceneLoader.instance.LoadScene(SceneLoader.sceneName.Main);
        mainAudioSource.PlayOneShot(PushButtonSound);
    }
}
