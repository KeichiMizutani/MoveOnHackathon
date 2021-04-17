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
    gameState currentGameState; //現在のゲームステート

    //イベント宣言
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

    //ステートをReadyに変更してReadyイベントに登録されている処理を実行させる
    public void ReceiveStateReadyNotify()
    {
        SetState(gameState.Ready);
        StateReadyHandler?.Invoke();
       
    }

    //ステートをPlayに変更してPlayイベントに登録されている処理を実行させる
    public void ReceiveStatePlayNotify()
    {
        SetState(gameState.Play);
        StatePlayHandler?.Invoke();
    }

    //ステートを設定する
    void SetState(gameState state)
    {
        currentGameState = state;
        Debug.Log("ゲームステートを" + currentGameState + "に変更しました");
    }

}
