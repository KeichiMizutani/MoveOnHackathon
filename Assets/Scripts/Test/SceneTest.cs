using UnityEngine;

public class SceneTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneLoader.instance.LoadScene(SceneLoader.sceneName.Title);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
