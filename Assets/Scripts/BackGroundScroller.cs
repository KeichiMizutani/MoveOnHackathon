using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroller : MonoBehaviour
{

    // === 内部データ ======================================================================
    [SerializeField] GameObject[] backgrounds = new GameObject[3]; //左から順に入れる
    float backgroundPosX = 0.0f;
    [SerializeField] PlayerController playerCtrl;

    // === コード(MonoBehaviour) ===========================================================

    void Awake() {
        backgroundPosX = backgrounds[2].transform.position.x;
    }
    void Start()
    {
        
    }
    void Update()
    {
        foreach(GameObject background in backgrounds){
            background.transform.position += 
                transform.right * (playerCtrl.rigidbody2D.velocity.x / 3) * Time.deltaTime;

            if(background.transform.position.x > backgroundPosX * 2){
                background.transform.position = new Vector3(-backgroundPosX, 0, 0);
            }else if(background.transform.position.x < -backgroundPosX * 2){
                background.transform.position = new Vector3(backgroundPosX, 0, 0);
            }
        }
    }

    
}
