using UnityEngine;
using UnityEngine.UI;

public class TItleUI : MonoBehaviour
{
    [SerializeField]
  Text  myHighScoreText; //�n�C�X�R�A��\������e�L�X�g
    [SerializeField]
    AudioSource mainAudioSource;
    [SerializeField]
    AudioClip PushButtonSound;

    private void Start()
    {
        //�ۑ�����Ă���n�C�X�R�A�̒l��Ăт����ăe�L�X�g�ɕ\��
        myHighScoreText.text = "YOUR HIGH SCORE:" + PrefasWrapper.GetSaveData(PrefasWrapper.KEY.Score).ToString();
    }

    //�X�^�[�g�{�^�����

    public void PushStartBtn()
    {
        SceneLoader.instance.LoadScene(SceneLoader.sceneName.Main);
        mainAudioSource.PlayOneShot(PushButtonSound);
    }
}
