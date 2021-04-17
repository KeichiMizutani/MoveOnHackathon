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

    //�V���O���g����
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

    //���݂̃V�[���ԍ����擾���ĕԂ�
    int GetCutrrentSceneNum()
    {
        int sceneNum = SceneManager.GetActiveScene().buildIndex;
        return sceneNum;
    }

    //�V�[�������[�h����
    public void LoadScene(sceneName scene)
    {
        SceneManager.LoadScene((int)scene);
    }

    //�����V�[�����J��Ԃ�
    void ReloadScene()
    {
        SceneManager.LoadScene(GetCutrrentSceneNum());
    }


}
