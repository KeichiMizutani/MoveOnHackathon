using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Events;

public class StageTimer : MonoBehaviour
{
    float timeLimit = 60.0f; //��������
    float TimeLimit
    {
        get
        {
            return timeLimit;
        }
    }�@//�������ԎQ�Ɨp
    int countDownTimer = 3; //�V�[���ړ����Ă���̃J�E���g�_�E��
    int countDownInterVal = 1; //�J�E���g�_�E���̑҂Ԋu

    bool isCount = false; //���J�E���g�_�E�����ł��邩

    public event System.Action EndCountDownHandler;�@//�J�E���g�_�E�����I���������Ƃ�ʒm���邽�߂̃C�x���g
    public event System.Action OverTimeLimitHandler; //�������ԏI��

    private void Awake()
    {
        //�C�x���g�o�^
        GameStateManager.instance.StateReadyHandler += Start;
        GameStateManager.instance.StatePlayHandler += CountDownTimeLimit;
        EndCountDownHandler += GameStateManager.instance.ReceiveStatePlayNotify;
        OverTimeLimitHandler += GameStateManager.instance.ReceiveStateEndNotify;
    }


    void Start()
    { 
        //�J�E���g�_�E�����J�n����
        StartCoroutine("PlayCountDown");
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
    
    //�Q�[���X�^�[�g�܂ł̃J�E���g�_�E��
    IEnumerator PlayCountDown()
    {
        for(int i = countDownTimer; i > 0; i--)
        {
            yield return new WaitForSeconds(countDownInterVal);
            Debug.Log(i);
        }
        //�J�E���g�_�E�����I���������Ƃ�ʒm����
        EndCountDownHandler?.Invoke();
    }
    
}