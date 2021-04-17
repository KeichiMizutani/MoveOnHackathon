using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    static public SceneLoader instance;
public   enum sceneName
    {
        Title,
        Main,
        Result
    }

    //シングルトン化
    private void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    //現在のシーン番号を取得して返す
    int GetCutrrentSceneNum()
    {
        int sceneNum = SceneManager.GetActiveScene().buildIndex;
        return sceneNum;
    }

    //シーンをロードする
    public void LoadScene(sceneName scene)
    {
        SceneManager.LoadScene((int)scene);
    }

    //同じシーンを繰り返す
    void ReloadScene()
    {
        SceneManager.LoadScene(GetCutrrentSceneNum());
    }


}
