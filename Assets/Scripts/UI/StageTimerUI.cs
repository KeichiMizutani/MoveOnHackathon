using UnityEngine;
using UnityEngine.UI;

public class StageTimerUI : MonoBehaviour
{
    [SerializeField]
    Text timeLimitText;
    StageTimer stageTimer;

    // Start is called before the first frame update
    void Start()
    {
       stageTimer =    GetComponent<StageTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        //�������Ԃ��e�L�X�g�ɕ\����������
        timeLimitText.text = "TIME LIMIT:" + stageTimer.TimeLimit.ToString("f2");
    }
}
