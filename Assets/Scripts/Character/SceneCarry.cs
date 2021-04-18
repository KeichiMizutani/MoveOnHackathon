using UnityEngine;
using UnityEngine.Events;

public class SceneCarry : MonoBehaviour
{
    static SceneCarry instance; //�C���X�^���X��
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //�C�x���g�o�^
        GameStateManager.instance.StateEndHandler += CarryResultScene;
    }

    //���U���g�V�[���ֈړ�
    void CarryResultScene()
    {
        SceneLoader.instance.LoadScene(SceneLoader.sceneName.Result);
    }
}
