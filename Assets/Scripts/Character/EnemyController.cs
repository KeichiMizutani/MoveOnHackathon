using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseCharacterController  
{
    // === 外部パラメータ(Inspector表示) ============================
    public float initSpeed = 6.0f;
    public bool jumpActionEnabled = true;

    // === 外部パラメータ ===========================================
    [System.NonSerialized] public bool attackEnable = false;
    
    //アニメーションのハッシュ名とかここで取得したらいいかも

    // === キャッシュ ==============================================
    PlayerController playerCtrl;

    // === コード(MonoBehaviour基本機能の実装) ======================
    protected override void Awake()
    {
        base.Awake();

        playerCtrl = GetComponent<PlayerController>();
        speed = initSpeed;
    }

    protected override void  FixedUpdateCharacter()
    {
        // ジャンプチェック
        if(jumped)
        {
            //着地チェック(接地判定と接地と時間による判定)
            if((grounded && !groundedPrev) || (grounded && Time.fixedTime > jumpStartTime + 1.0f))
            {
                jumped = false;
            }

            if(Time.fixedTime > jumpStartTime + 1.0f)
            {
                if(rigidbody2D.gravityScale < gravityScale){
                    rigidbody2D.gravityScale = gravityScale;
                }
            }
        }
        else
        {
            rigidbody2D.gravityScale = gravityScale;
        }

        // キャラの方向
        transform.localScale = new Vector3(baseScaleX * dir, transform.localScale.y, transform.localScale.z);
    }

    // === コード ============================================================
    public bool ActionJump(){
        if(jumpActionEnabled&& grounded && !jumped){
            rigidbody2D.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
            jumped = true;
            jumpStartTime = Time.fixedTime;
        }

        return jumped;
    }

    public void ActionAttack(){
        attackEnable = true;

        //実装未定
    }

    public override void Dead(bool gameOver)
    {
        base.Dead(gameOver);
        Destroy(gameObject, 1.0f);//<--ObjectBoolにしたいお気持ち
    }

}
