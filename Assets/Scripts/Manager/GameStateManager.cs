using UnityEngine;
using UnityEngine.Events;

public enum gameState
{
    Ready,
    Play,
    End,
    GameOver,
    Pose
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;
    gameState currentGameState; //���݂̃Q�[���X�e�[�g

    //�C�x���g�錾
    public event System.Action StateReadyHandler; //Ready�̃C�x���g
    public event System.Action StatePlayHandler; //Play�̃C�x���g
    public event System.Action StateEndHandler; //End�̃C�x���g

    //�V���O���g����
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        ReceiveStateReadyNotify();
    }

    //�X�e�[�g��Ready�ɕύX����Ready�C�x���g�ɓo�^����Ă��鏈�������s������
    public void ReceiveStateReadyNotify()
    {
        SetState(gameState.Ready);
        StateReadyHandler?.Invoke();
    }

    //�X�e�[�g��Play�ɕύX����Play�C�x���g�ɓo�^����Ă��鏈�������s������
    public void ReceiveStatePlayNotify()
    {
        SetState(gameState.Play);
        StatePlayHandler?.Invoke();
    }

    //�X�e�[�g��End�ɕύX����Play�C�x���g�ɓo�^����Ă��鏈�������s������
    public void ReceiveStateEndNotify()
    {
        SetState(gameState.End);
        StateEndHandler?.Invoke();
    }

    //�X�e�[�g��ݒ肷��
    void SetState(gameState state)
    {
        currentGameState = state;
        //Debug.Log("�Q�[���X�e�[�g��" + currentGameState + "�ɕύX���܂���");
    }

}
