using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseCharacterController  
{
    // === 外部パラメータ(Inspector表示) ============================
    public float initSpeed = 6.0f;
    public float initJumpPower = 7.0f;
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
        jumpPower = initJumpPower;
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
            rigidbody2D.velocity = Vector2.up * jumpPower;
            jumped = true;
            jumpStartTime = Time.fixedTime;
        }

        return jumped;
    }

    public void ActionAttack(){
        attackEnable = true;

        //実装未定
    }

    public override void ActionMove(float n)
    {
        if (!activeStatus)
        {
            return;
        }

        // 初期化
        float dirOld = dir;
        float moveSpeed = Mathf.Clamp(Mathf.Abs(n), -1.0f, 1.0f);

         // 移動チェック
        if(n != 0.0f)
        {
            dir = Mathf.Sign(n);
            moveSpeed = (moveSpeed < 0.5f) ? (moveSpeed * (1.0f / 0.5f)) : 1.0f;
            speedVx = initSpeed * moveSpeed * dir;
        }
    }

    public override void Dead(bool gameOver)
    {
        base.Dead(gameOver);
        Destroy(gameObject, 1.0f);//<--ObjectBoolにしたいお気持ち
    }

}
