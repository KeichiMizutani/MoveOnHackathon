using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseCharacterController : MonoBehaviour
{

    //=== 外部パラメータ ====================================
    public Vector2 velocityMin = new Vector2(-100.0f, -100.0f); //それぞれx,yのスピード方向の最小値
    public Vector2 velocityMax = new Vector2(100.0f, 50.0f);    //それぞれx,yのスピード方向の最大値

    [System.NonSerialized] public float dir = 1.0f;             //角度 sinを用いることでキャラの向いてる方向を取得
    [System.NonSerialized] public float speed = 6.0f;           //キャラの移動速度
    [System.NonSerialized] public float baseScaleX = 1.0f;      //キャラのtransformのscaleのx
    [System.NonSerialized] public bool activeStatus = false;    //アクティブかどうか
    [System.NonSerialized] public bool jumped = false;          //ジャンプしているかどうか
    [System.NonSerialized] public float jumpPower = 7.0f;       //ジャンプの強さ
    [System.NonSerialized] public bool grounded = false;        //着地しているかどうか
    [System.NonSerialized] public bool groundedPrev = false;    //前フレームの接地判定
    [System.NonSerialized] public new Rigidbody2D rigidbody2D;  //Rigidbody2D

    //=== キャッシュ =======================================
    protected Transform groundCheckL;                           //着地判定に使う子オブジェクトの左側
    protected Transform groundCheckC;                           //着地判定に使う子オブジェクトの中央
    protected Transform groundCheckR;                           //着地判定に使う子オブジェクトの右側

    //=== 内部パラメータ ====================================
    protected float speedVx = 0.0f;                             //x方向のスピードを格納
    //protected float speedVxAddPower = 0.0f;                     //
    protected float gravityScale = 1.0f;                        //重力のスケール
    protected float jumpStartTime = 0.0f;                       //狭いところでジャンプしたときに時間で強制的に着地判定を行うための時間を格納した変数

    protected GameObject groundCheckOnRoadObject;               //ステージのGameObjectを格納
    protected GameObject groundCheckOnEnemyObject;              //敵や障害物のGameObjectを格納

    //=== イベントの設定 ====================================
   bool isMove = false; //キャラクターが動けるかどうか

    //=== コード(Monobehaviour基本機能の実装) ================
    protected virtual void Awake()
    {
        groundCheckL = transform.Find("GroundCheck_Left");
        groundCheckC = transform.Find("GroundCheck_Center");
        groundCheckR = transform.Find("GroundCheck_Right");

        dir = (transform.localScale.x > 0.0f) ? 1 : -1;
        baseScaleX = transform.localScale.x * dir;
        transform.localScale = new Vector3(-1 * baseScaleX, transform.localScale.y, transform.localScale.z);

        activeStatus = true;
        rigidbody2D = GetComponent<Rigidbody2D>();
        gravityScale = rigidbody2D.gravityScale;
    }

    protected virtual void Start()
    {
        GameStateManager.instance.StatePlayHandler += StartMove;
        GameStateManager.instance.StateEndHandler += StopMove;
    }

    protected virtual void Update()
    {

    }



    protected virtual void FixedUpdate()
    {
        //動けるかどうかチェック
      if (isMove)
      {
        // 落下チェック
        if (transform.position.y < -30f)
        {

        }

        // 地面チェック
        groundedPrev = grounded;
        grounded = false;

        groundCheckOnRoadObject = null;
        groundCheckOnEnemyObject = null;

        Collider2D[][] groundCheckCollider = new Collider2D[3][];
        groundCheckCollider[0] = Physics2D.OverlapPointAll(groundCheckL.position);
        groundCheckCollider[1] = Physics2D.OverlapPointAll(groundCheckC.position);
        groundCheckCollider[2] = Physics2D.OverlapPointAll(groundCheckR.position);

        foreach (Collider2D[] groundCheckList in groundCheckCollider)
        {
            foreach (Collider2D groundCheck in groundCheckList)
            {
                if (groundCheck != null)
                {
                    if (!groundCheck.isTrigger)
                    {
                        grounded = true;

                        if (groundCheck.tag == "Road")
                        {
                            groundCheckOnRoadObject = groundCheck.gameObject;
                        }
                        else if (groundCheck.tag == "Enemy")
                        {
                            groundCheckOnEnemyObject = groundCheck.gameObject;
                        }

                    }
                }
            }
        }

        // キャラクター個別の処理
        FixedUpdateCharacter();

        // 移動計算
        rigidbody2D.velocity = new Vector2(speedVx, rigidbody2D.velocity.y);

        float vx = Mathf.Clamp(rigidbody2D.velocity.x, velocityMin.x, velocityMax.x);
        float vy = Mathf.Clamp(rigidbody2D.velocity.y, velocityMin.y, velocityMax.y);
        rigidbody2D.velocity = new Vector2(vx, vy);
    }
    }

    protected virtual void FixedUpdateCharacter()
    {

    }

    protected virtual void OnCollisionEnter2D(Collision2D other) {
        
    }

    //=== コード(基本アクション) ===========================

    public virtual void ActionMove(float n)
    {
        if(n != 0.0f)
        {
            dir = Mathf.Sign(n);
            speedVx = speed * n;
            //animation使うならここ
        }
        else
        {
            speedVx = 0.0f;
            //animation使うならここ
        }
    }

    public virtual void Dead()
    {
        if (!activeStatus)
        {
            return;
        }
        activeStatus = false;
        // アニメーションはここ
    }

    //キャラクターが動き始めるイベント処理開始
    void StartMove()
    {
        isMove = true;
    }

    //キャラクターを止めるイベント処理開始
    void StopMove()
    {
        isMove = false;
    }

}
