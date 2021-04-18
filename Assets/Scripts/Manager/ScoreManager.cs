using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
public    static ScoreManager instance; //インスタンス化

    //=== 内部パラメータ ====================================
    int myScore = 0; //所持スコア
    int highScore = 0;

    //(外部参照用)
    public int MyScore
    {
        get
        {
            return myScore;
        }
    }

    public int HighScore
    {
        get
        {
            return highScore;
        }
    }

    //=== 調整パラメータ ====================================
    [SerializeField]
    int clearScore = 1000; //クリアスコア
    [SerializeField]
    int breakScore = 100; //障害物を壊したときのスコア

    //=== イベント登録 ====================================


    //=== コード ====================================

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

    // Start is called before the first frame update
    void Start()
    {
        myScore = 0; //初期値をリセット
        //ステージクリア時にスコアが加算される
        GameStateManager.instance.StateEndHandler += PlusClearScore;
    }

    //障害物を壊したときに自分のスコアに加算する
  public  void PlusBreakScore()
    {
        myScore += breakScore;
    }

    //クリアしたときに自分のスコアに加算する
    void PlusClearScore()
    {
        myScore += clearScore;
    }

    //スコアを保存する
    void SaveScore()
    {
        PrefasWrapper.SetSaveData(PrefasWrapper.KEY.Score, MyScore);
    }
}
