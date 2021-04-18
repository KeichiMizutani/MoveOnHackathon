using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [SerializeField]
 Text   resultScoreText; //���U���g�X�R�A��\������e�L�X�g

    private void Start()
    {
        //���ʃX�R�A���e�L�X�g�ɕ\��
        resultScoreText.text = ScoreManager.instance.MyScore.ToString();
    }

    //�^�C�g���֖߂�
    public void PushBackTitleBtn()
    {
        SceneLoader.instance.LoadScene(SceneLoader.sceneName.Title);
    }

    //������x�Q�[�����v���C����
    public void PushRetryGameBtn()
    {
        SceneLoader.instance.LoadScene(SceneLoader.sceneName.Main);
    }
}
