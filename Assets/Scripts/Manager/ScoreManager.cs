using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
public    static ScoreManager instance; //�C���X�^���X��

    //=== �����p�����[�^ ====================================
    int myScore = 0; //�����X�R�A
    int highScore = 0;

    //(�O���Q�Ɨp)
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

    //=== �����p�����[�^ ====================================
    [SerializeField]
    int clearScore = 1000; //�N���A�X�R�A
    [SerializeField]
    int breakScore = 100; //��Q�����󂵂��Ƃ��̃X�R�A

    //=== �C�x���g�o�^ ====================================


    //=== �R�[�h ====================================

    //�V���O���g����
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
        myScore = 0; //�����l�����Z�b�g
        //�X�e�[�W�N���A���ɃX�R�A�����Z�����
        GameStateManager.instance.StateEndHandler += PlusClearScore;
    }

    //��Q�����󂵂��Ƃ��Ɏ����̃X�R�A�ɉ��Z����
  public  void PlusBreakScore()
    {
        myScore += breakScore;
        Debug.Log(myScore);
    }

    //�N���A�����Ƃ��Ɏ����̃X�R�A�ɉ��Z����
    void PlusClearScore()
    {
        myScore += clearScore;
    }

    //�X�R�A��ۑ�����
    void SaveScore()
    {
        PrefasWrapper.SetSaveData(PrefasWrapper.KEY.Score, MyScore);
    }
}
