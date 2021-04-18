using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ScoreCountUI : MonoBehaviour
{
    [SerializeField]
     Text scoreText;
    [SerializeField]
    Text highScoreUpdateText; //�n�C�X�R�A���X�V�����Ƃ��ɕ\�������e�L�X�g

    int sec = 1; //�e�L�X�g�\���b��


    void Start()
    {
        //�n�C�X�R�A���X�V�����Ƃ��ɕ\�������e�L�X�g���ŏ��͔�\���ɂ���
        highScoreUpdateText.gameObject.SetActive(false);
    }

    void Update()
    {
        //�����X�R�A��UI�ɔ��f��������
        scoreText.text = "Your Score: " + ScoreManager.instance.MyScore.ToString();

        //���������X�R�A���n�C�X�R�A�𒴂�����n�C�X�R�A�X�V�e�L�X�g��\������
        if(ScoreManager.instance.MyScore > ScoreManager.instance.HighScore)
        {
            StartCoroutine("ShowUpdateHighScoreText");
        }
    }

    //�n�C�X�R�A�X�V�����Ƃ��̒ʒm����莞�ԕ\��������
    IEnumerator ShowUpdateHighScoreText()
    {
        highScoreUpdateText.gameObject.SetActive(true);
        yield return new WaitForSeconds(sec);
        highScoreUpdateText.gameObject.SetActive(false);
    }
    
}
