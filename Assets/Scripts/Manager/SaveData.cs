using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PrefasWrapper
{
    /*
     PlayerPrefsの使用方法
       ・値を読み込む
    　　   PlayerPrefs.Get○○("キー");
   　・値をセットする、保存する
        　   PlayerPrefs.Set○○("キー", "値");←○○にはint,float,stringのいずれかが入る
         　  PlayerPrefs.Save();
         */

    // PlayerPrefsで使用できる型リスト
    private enum TYPE
    {
        INT,
        STRING,
        FLOAT
    }

    //PlayerPrefsで保存するもの
    public enum KEY
    {
        Score, //スコア
        Sound //音量設定
    }

    //PlayerPrefsで管理しているデータリスト
    private static Dictionary<KEY, TYPE> saveList = new Dictionary<KEY, TYPE>()
    {
        {KEY.Score,TYPE.INT }, //スコア
        {KEY.Sound,TYPE.STRING }　//音量設定
    };

    //保存したいデータを設定する
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
        Debug.Log("データを保存しました");
    }

    //保存した値を読み込む
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
                Debug.Log("データを読み込みました");
                return null;
        }
    }
}

public class SaveData : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("現在保存されているスコアのデータは" + PrefasWrapper.GetSaveData(PrefasWrapper.KEY.Score) + "です");
    }

    void SaveScore()
    {
        PrefasWrapper.SetSaveData(PrefasWrapper.KEY.Score, 100);
    }
}
