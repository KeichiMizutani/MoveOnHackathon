using UnityEngine;

public class ResultUI : MonoBehaviour
{
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
