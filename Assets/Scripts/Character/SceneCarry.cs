using UnityEngine;
using UnityEngine.Events;

public class SceneCarry : MonoBehaviour
{
    static SceneCarry instance; //インスタンス化
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //イベント登録
        GameStateManager.instance.StateEndHandler += CarryResultScene;
    }

    //リザルトシーンへ移動
    void CarryResultScene()
    {
        SceneLoader.instance.LoadScene(SceneLoader.sceneName.Result);
    }
}
