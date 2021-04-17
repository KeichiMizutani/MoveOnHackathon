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

    public event System.Action EndCountDownHandler;�@//�J�E���g�_�E�����I���������Ƃ�ʒm���邽�߂̃C�x���g

    private void Awake()
    {
        
        //�C�x���g�o�^
        GameStateManager.instance.StateReadyHandler += Start;
        EndCountDownHandler += GameStateManager.instance.ReceiveStatePlayNotify;
    }


    void Start()
    { 
        //�J�E���g�_�E�����J�n����
        StartCoroutine("PlayCountDown");

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
