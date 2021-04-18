using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class PrefasWrapper
{
    /*
     PlayerPrefs�̎g�p���@
       �E�l��ǂݍ���
    �@�@   PlayerPrefs.Get����("�L�[");
   �@�E�l���Z�b�g����A�ۑ�����
        �@   PlayerPrefs.Set����("�L�[", "�l");�������ɂ�int,float,string�̂����ꂩ������
         �@  PlayerPrefs.Save();
         */

    // PlayerPrefs�Ŏg�p�ł���^���X�g
    private enum TYPE
    {
        INT,
        STRING,
        FLOAT
    }

    //PlayerPrefs�ŕۑ��������
    public enum KEY
    {
        Score, //�X�R�A
        Sound //���ʐݒ�
    }

    //PlayerPrefs�ŊǗ����Ă���f�[�^���X�g
    private static Dictionary<KEY, TYPE> saveList = new Dictionary<KEY, TYPE>()
    {
        {KEY.Score,TYPE.INT }, //�X�R�A
        {KEY.Sound,TYPE.STRING } //���ʐݒ�
    };

    //�ۑ��������f�[�^��ݒ肷��
    public static void SetSaveData(KEY key, object value)
    {
        if (value is int)
        {
            PlayerPrefs.SetInt(key.ToString(), (int)value);
        }
        else if (value is float)
        {
            PlayerPrefs.SetFloat(key.ToString(), (float)value);
        }
        else if (value is string)
        {
            PlayerPrefs.SetString(key.ToString(), (string)value);
        }
        PlayerPrefs.Save();
        Debug.Log("�f�[�^��ۑ����܂���");
    }

    //�ۑ������l��ǂݍ���
    public static object GetSaveData(KEY key)
    {
        switch (saveList[key])
        {
            case TYPE.INT:
                return PlayerPrefs.GetInt(key.ToString());
            case TYPE.FLOAT:
                return PlayerPrefs.GetFloat(key.ToString());
            case TYPE.STRING:
                return PlayerPrefs.GetString(key.ToString());
            default:
                Debug.Log("�f�[�^��ǂݍ��݂܂���");
                return null;
        }
    }
}

public class SaveData : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("���ݕۑ�����Ă���X�R�A�̃f�[�^��" + PrefasWrapper.GetSaveData(PrefasWrapper.KEY.Score) + "�ł�");
    }

}
