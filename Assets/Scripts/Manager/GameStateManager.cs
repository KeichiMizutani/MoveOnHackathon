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
    gameState currentGameState; //現在のゲームステート

    //イベント宣言
    public event System.Action StateReadyHandler; //Readyのイベント
    public event System.Action StatePlayHandler; //Playのイベント
    public event System.Action StateEndHandler;　//Endのイベント

    //シングルトン化
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

    //ステートをEndに変更してPlayイベントに登録されている処理を実行させる
    public void ReceiveStateEndNotify()
    {
        SetState(gameState.End);
        StateEndHandler?.Invoke();
    }

    //ステートを設定する
    void SetState(gameState state)
    {
        currentGameState = state;
        Debug.Log("ゲームステートを" + currentGameState + "に変更しました");
    }

}
