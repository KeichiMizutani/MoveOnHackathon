using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Events;

public class StageTimer : MonoBehaviour
{
    float timeLimit = 60.0f; //制限時間
    float TimeLimit
    {
        get
        {
            return timeLimit;
        }
    }　//制限時間参照用
    int countDownTimer = 3; //シーン移動してからのカウントダウン
    int countDownInterVal = 1; //カウントダウンの待つ間隔

    public event System.Action EndCountDownHandler;　//カウントダウンが終了したことを通知するためのイベント

    private void Awake()
    {
        
        //イベント登録
        GameStateManager.instance.StateReadyHandler += Start;
        EndCountDownHandler += GameStateManager.instance.ReceiveStatePlayNotify;
    }


    void Start()
    { 
        //カウントダウンを開始する
        StartCoroutine("PlayCountDown");

    }

    
    //ゲームスタートまでのカウントダウン
    IEnumerator PlayCountDown()
    {
        for(int i = countDownTimer; i > 0; i--)
        {
            yield return new WaitForSeconds(countDownInterVal);
            Debug.Log(i);
        }

        //カウントダウンが終了したことを通知する
        EndCountDownHandler?.Invoke();
    }
    
}
