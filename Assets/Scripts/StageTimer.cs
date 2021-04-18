using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

public class StageTimer : MonoBehaviour
{
    float timeLimit = 60.0f; //��������
   public float TimeLimit
    {
        get
        {
            return timeLimit;
        }
    }//�������ԎQ�Ɨp
    int countDownTimer = 3; //�V�[���ړ����Ă���̃J�E���g�_�E��
    public int CountDownTimer
    {
        get
        {
            return countDownTimer;
        }
    }
    int countDownInterVal = 1; //�J�E���g�_�E���̑҂Ԋu

    bool isCount = false; //���J�E���g�_�E�����ł��邩

    public event System.Action EndCountDownHandler;//�J�E���g�_�E�����I���������Ƃ�ʒm���邽�߂̃C�x���g
    public event System.Action OverTimeLimitHandler; //�������ԏI��

    [SerializeField]
    AudioClip[] countDownSE = new AudioClip[4];   //�J�E���g�_�E����SE
    [SerializeField]
    AudioSource mainAudioSource; //�I�[�f�B�I�\�[�X
    [SerializeField]
    AudioSource bgmAudioSource; //BGM
    [SerializeField]
    Text countDownText;

    private void Start()
    {
        //�C�x���g�o�^
        GameStateManager.instance.StateReadyHandler += StartCountDown;
        GameStateManager.instance.StatePlayHandler += CountDownTimeLimit;
        EndCountDownHandler += GameStateManager.instance.ReceiveStatePlayNotify;
        OverTimeLimitHandler += GameStateManager.instance.ReceiveStateEndNotify;

        timeLimit = 60.0f;
    }

    void Update()
    {
        //�����J�E���g�_�E���ł����ԂȂ�
        if (isCount)
        {
            //�������Ԃ��J�n����
            timeLimit -= Time.deltaTime;
            Debug.Log(timeLimit);

            //�������Ԃ��[���ɂȂ����琧�����ԏI���̒ʒm�𔭍s����
            if (timeLimit <= 0)
            {
                isCount = false;
                OverTimeLimitHandler?.Invoke();
            }
        }
    }

    //�J�E���g�_�E���ł��邩�ǂ�����؂�ւ���
    void CountDownTimeLimit()
    {
        isCount = true;
    }

    //�Q�[���J�n�܂ł̃J�E���g�_�E�����X�^�[�g������
    void StartCountDown()
    {
        //�J�E���g�_�E�����J�n����
        StartCoroutine("CountDown");
        Debug.Log("Start");
    }
    
    //�Q�[���X�^�[�g�܂ł̃J�E���g�_�E��
    IEnumerator CountDown()
    {
        for (int i = countDownTimer; i >= 0; i--)
        {
            mainAudioSource.PlayOneShot(countDownSE[countDownTimer - i]);
            Debug.Log(countDownSE[countDownTimer - i]);
            countDownText.text = i.ToString();
            yield return new WaitForSeconds(countDownInterVal);
        }

        bgmAudioSource.Play();
        
        countDownText.gameObject.SetActive(false);
        //�J�E���g�_�E�����I���������Ƃ�ʒm����
        EndCountDownHandler?.Invoke();
    }
    
}
