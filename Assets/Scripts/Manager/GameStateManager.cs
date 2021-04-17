using UnityEngine;
using UnityEngine.Events;

public enum gameState
{
    Ready,
    Play,
    Goal,
    GameOver,
    Pose
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;
    gameState currentGameState; //���݂̃Q�[���X�e�[�g

    //�C�x���g�錾
    public event System.Action StateReadyHandler;
    public event System.Action StatePlayHandler;

    private void Awake()
    {
        instance = this;
        
    }

    private void Start()
    {
        ReceiveStateReadyNotify();
        StageTimer stageTimer = new StageTimer();
        stageTimer.EndCountDownHandler += ReceiveStatePlayNotify;

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

    //�X�e�[�g��ݒ肷��
    void SetState(gameState state)
    {
        currentGameState = state;
        Debug.Log("�Q�[���X�e�[�g��" + currentGameState + "�ɕύX���܂���");
    }

}
