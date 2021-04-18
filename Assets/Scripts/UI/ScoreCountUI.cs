using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ScoreCountUI : MonoBehaviour
{
    [SerializeField]
     Text scoreText;
    [SerializeField]
    Text highScoreUpdateText; //ハイスコアを更新したときに表示されるテキスト

    int sec = 1; //テキスト表示秒数


    void Start()
    {
        //ハイスコアを更新したときに表示されるテキストを最初は非表示にする
        highScoreUpdateText.gameObject.SetActive(false);
    }

    void Update()
    {
        //所持スコアをUIに反映し続ける
        scoreText.text =　"Your Score: " + ScoreManager.instance.MyScore.ToString();

        //もし所持スコアがハイスコアを超えたらハイスコア更新テキストを表示する
        if(ScoreManager.instance.MyScore > ScoreManager.instance.HighScore)
        {
            StartCoroutine("ShowUpdateHighScoreText");
        }
    }

    //ハイスコア更新したときの通知を一定時間表示させる
    IEnumerator ShowUpdateHighScoreText()
    {
        highScoreUpdateText.gameObject.SetActive(true);
        yield return new WaitForSeconds(sec);
        highScoreUpdateText.gameObject.SetActive(false);
    }
    
}
