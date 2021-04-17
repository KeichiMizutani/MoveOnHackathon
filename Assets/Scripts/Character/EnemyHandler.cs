using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyHandler : MonoBehaviour
{
     // === キャッシュ =====================================================
    protected EnemyController enemyCtrl;
    public GameObject player;                          //<-これ後でInspecterから消します
    protected PlayerController playerCtrl;

    // === 内部パラメータ =================================================
    protected float distanceToPlayer = 0.0f;
    protected float distanceToPlayerPrev = 0.0f;

    // === コード(MonoBehaviour基本機能の実装) =============================
    public virtual void Awake() 
    {
        enemyCtrl = GetComponent<EnemyController>();
        playerCtrl = player.GetComponent<PlayerController>();
    }

    public virtual void Start() 
    {
        
    }

    public virtual void Update() 
    {
        
    }

    public virtual void FixedUpdate() 
    {
        Vector2 playerDir = GetPlayerDir();
        enemyCtrl.ActionMove(playerDir.x); 

        if(playerDir.y > 0.3f || enemyCtrl.rigidbody2D.velocity.magnitude < 0.05f){
            enemyCtrl.ActionJump();
        }


        FixedUpdateAI();
    }

    public virtual void FixedUpdateAI() 
    {
         
    }
    
    // === AIサポート ================================
    public Vector2 GetPlayerDir(){
        Vector2 dirVec =  player.transform.position - this.transform.position;
        return dirVec.normalized;
    }
    
}
