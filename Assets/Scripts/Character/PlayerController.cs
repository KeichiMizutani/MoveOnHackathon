using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseCharacterController
{
    // ===  外部パラメータ(Inspector表示) ============================
    public float initHpMax = 3.0f;
    [Range(0.1f, 50.0f)] public float initSpeed = 12.0f;        //横移動のスピード
    [Range(-15.0f, -5.0f)] public float cameraOffset = -10.0f;  //カメラとの距離



    // ===  内部パラメータ ============================
    int jumpCount = 0;              //ジャンプした回数
    bool breakEnabled = true;       //ブレーキするかしないか
    float groundFriction = 0.8f;    //地面との滑りやすさ

    float hp = 3.0f;                //HPの値
    float hpMax = 3.0f;             //HPの最大値

    // === コード(Monobehaviour基本機能の実装) =========
    protected override void Awake()
    {
        base.Awake();

        // パラメータ初期化
        speed = initSpeed;
        SetHP(initHpMax, initHpMax);
    }

    protected override void Update()
    {
        if(hp <= 0){
            Dead();
        }
    }

    protected override void FixedUpdateCharacter()
    {
        // 着地チェック
        if (jumped)
        {
            if((grounded && ! groundedPrev) || (grounded && Time.fixedTime > jumpStartTime + 1.0f))
            {
                jumped = false;
                jumpCount = 0;
            }
        }
        else
        {
            jumpCount = 0;
            rigidbody2D.gravityScale = gravityScale;
        }

        // キャラの方向
        transform.localScale = new Vector3(baseScaleX * dir, transform.localScale.y, transform.localScale.z);

        // ジャンプ中の処理
        if(jumped && grounded)
        {
            if (breakEnabled)
            {
                breakEnabled = false;
                speedVx *= 0.9f;
            }
        }

        // 移動減速処理
        if (breakEnabled)
        {
            speedVx *= groundFriction;
        }

        // カメラ
        Camera.main.transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y, 0) + Vector3.forward * cameraOffset;
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy"){
            ActionDamage(1.0f);
        }
        else if
        (other.gameObject.tag == "Boss"){
            ActionDamage(5.0f);
        }
    }

    // === コード(基本アクション)=====================================
    public override void ActionMove(float n)
    {
        if (!activeStatus)
        {
            return;
        }

        // 初期化
        float dirOld = dir;
        breakEnabled = false;
        float moveSpeed = Mathf.Clamp(Mathf.Abs(n), -1.0f, 1.0f);

        // 移動チェック
        if(n != 0.0f)
        {
            dir = Mathf.Sign(n);
            moveSpeed = (moveSpeed < 0.5f) ? (moveSpeed * (1.0f / 0.5f)) : 1.0f;
            speedVx = initSpeed * moveSpeed * dir;
        }
        else
        {
            breakEnabled = true;
        }

        // その場振り向きチェック
        if(dirOld != dir)
        {
            breakEnabled = true;
        }
    }

    public void ActionJump()
    {
        switch (jumpCount)
        {
            case 0:
                if (grounded)
                {
                    // animation
                    rigidbody2D.velocity = Vector2.up * jumpPower;
                    jumpStartTime = Time.fixedTime;
                    jumped = true;
                    jumpCount++;
                }
                break;
            case 1:
                if (!grounded)
                {
                    // animation
                    rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpPower * 2 /3);
                    jumped = true;
                    jumpCount++;
                }
                break;
        }
    }

    public void ActionDamage(float _damage){
        hp -= _damage;
    }

    public void ActionAttack(){
        if(Input.GetMouseButtonDown(0)){
            GameObject clickedGameObject = null;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2D = Physics2D.Raycast((Vector2)ray.origin,(Vector2)ray.direction);
            if(hit2D){
                clickedGameObject = hit2D.transform.gameObject;
                if(clickedGameObject.gameObject.tag == "Liquid"){
                    Destroy(clickedGameObject);//<-ObjectPool はまた今度
                    ScoreManager.instance.PlusBreakScore();//スコア加算
                }
            }

        }
    }

    bool SetHP(float _hp, float _hpMax)
    {
        hp = _hp;
        hpMax = _hp;
        return (hp <= 0.0f);
    }
}
