using UnityEngine;

public class TItleUI : MonoBehaviour
{
    //�X�^�[�g�{�^��������
    public void PushStartBtn()
    {
        SceneLoader.instance.LoadScene(SceneLoader.sceneName.Main);
    }
}
