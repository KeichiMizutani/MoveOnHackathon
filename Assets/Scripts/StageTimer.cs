using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

public class StageTimer : MonoBehaviour
{
    float timeLimit = 60.0f; //制限時間
   public float TimeLimit
    {
        get
        {
            return timeLimit;
        }
    }　//制限時間参照用
    int countDownTimer = 3; //シーン移動してからのカウントダウン
    public int CountDownTimer
    {
        get
        {
            return countDownTimer;
        }
    }
    int countDownInterVal = 1; //カウントダウンの待つ間隔

    bool isCount = false; //今カウントダウンができるか

    public event System.Action EndCountDownHandler;　//カウントダウンが終了したことを通知するためのイベント
    public event System.Action OverTimeLimitHandler; //制限時間終了

    [SerializeField]
    AudioClip[] countDownSE = new AudioClip[2];   //カウントダウンのSE
    [SerializeField]
    AudioSource mainAudioSource; //オーディオソース
    [SerializeField]
    Text countDownText;

    private void Start()
    {
        //イベント登録
        GameStateManager.instance.StateReadyHandler += StartCountDown;
        GameStateManager.instance.StatePlayHandler += CountDownTimeLimit;
        EndCountDownHandler += GameStateManager.instance.ReceiveStatePlayNotify;
        OverTimeLimitHandler += GameStateManager.instance.ReceiveStateEndNotify;

        timeLimit = 60.0f;
    }

    void Update()
    {
        //もしカウントダウンできる状態なら
        if (isCount)
        {
            //制限時間を開始する
            timeLimit -= Time.deltaTime;
            Debug.Log(timeLimit);

            //制限時間がゼロになったら制限時間終了の通知を発行する
            if (timeLimit <= 0)
            {
                isCount = false;
                OverTimeLimitHandler?.Invoke();
            }
        }
    }

    //カウントダウンできるかどうかを切り替える
    void CountDownTimeLimit()
    {
        isCount = true;
    }

    //ゲーム開始までのカウントダウンをスタートさせる
    void StartCountDown()
    {
        //カウントダウンを開始する
        StartCoroutine("CountDown");
    }
    
    //ゲームスタートまでのカウントダウン
    IEnumerator CountDown()
    {
        for (int i = countDownTimer; i > 0; i--)
        {
            mainAudioSource.PlayOneShot(countDownSE[0]);
            countDownText.text =i.ToString();
            yield return new WaitForSeconds(countDownInterVal);
            Debug.Log(i);
        }
        mainAudioSource.PlayOneShot(countDownSE[1]);
        countDownText.gameObject.SetActive(false);
        //カウントダウンが終了したことを通知する
        EndCountDownHandler?.Invoke();
    }
    
}
