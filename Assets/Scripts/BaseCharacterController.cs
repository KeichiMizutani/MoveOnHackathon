using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterController : MonoBehaviour
{

    //=== 外部パラメータ ====================================
    public Vector2 velocityMin = new Vector2(-100.0f, -100.0f);//x,y
    public Vector2 velocityMax = new Vector2(100.0f, 50.0f);

    [HideInInspector] public float dir = 1.0f;
    [HideInInspector] public float speed = 6.0f;
    [HideInInspector] public float baseScaleX = 1.0f;       //
    [HideInInspector] public bool activeStatus = false;     //
    [HideInInspector] public bool jumped = false;           //
    [HideInInspector] public bool grounded = false;         //
    [HideInInspector] public bool groundedPrev = false;     //

    //=== キャッシュ =======================================
    protected Transform groundCheckL;
    protected Transform groundCheckC;
    protected Transform groundCheckR;

    //=== 内部パラメータ ====================================
    protected float speedVx = 0.0f;
    protected float speedVxAddPower = 0.0f;
    protected float gravityScale = 9.8f;
    protected float jumpStartTime = 0.0f;

    protected GameObject groundCheckOnRoadObject;           //
    protected GameObject groundCheckOnEnemyObject;          //

    protected new Rigidbody2D rigidbody2D;


    //=== コード(Monobehaviour基本機能の実装) ================
    protected virtual void Awake()
    {
        groundCheckL = transform.Find("GroundCheck_Left");
        groundCheckC = transform.Find("GroundCheck_Center");
        groundCheckR = transform.Find("GroundCheck_Right");

        dir = (transform.localScale.x > 0.0f) ? 1 : -1;
        baseScaleX = transform.localScale.x * dir;
        transform.localScale = new Vector3(baseScaleX, transform.localScale.y, transform.localScale.z);

        activeStatus = true;
        rigidbody2D = GetComponent<Rigidbody2D>();
        gravityScale = rigidbody2D.gravityScale;
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    protected virtual void FixedUpdate()
    {
        //地面チェック
        groundedPrev = grounded;
        grounded = false;

        groundCheckOnRoadObject = null;
        groundCheckOnEnemyObject = null;

        Collider2D[][] groundCheckCollider = new Collider2D[3][];
        groundCheckCollider[0] = Physics2D.OverlapPointAll(groundCheckL.position);
        groundCheckCollider[1] = Physics2D.OverlapPointAll(groundCheckC.position);
        groundCheckCollider[2] = Physics2D.OverlapPointAll(groundCheckR.position);

        foreach(Collider2D[] groundCheckList in groundCheckCollider)
        {
            foreach(Collider2D groundCheck in groundCheckList)
            {
                if(groundCheck != null)
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

    protected virtual void FixedUpdateCharacter()
    {

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
}
