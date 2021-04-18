using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZakoHandler : EnemyHandler
{
   // === 外部パラメータ(Inspector表示) ===============================
   public GameObject liquidObj;
   public float liquidVelocity = 5.0f;
   public Vector2 attackIntervalRange = new Vector2(3.0f, 6.0f);

   // === 内部パラメータ =============================================
   float attackTimer = 0.0f;
   float attackInterval = 0.0f;

   // === コード ====================================================
   public override void Awake() {
       base.Awake();
   }

    public override void Start()
    {
        SetAttackInterval();
    }

    public override void Update()
    {
        attackTimer += Time.deltaTime;
        if(attackTimer > attackInterval){
            SetAttackInterval();
            attackTimer = 0.0f;

            ActionAttack();
        }

    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void FixedUpdateAI()
    {

    }

    // === 攻撃関連 =======================================================

    // ザコとプレイヤーのX座標の距離を返す
    private float GetPlayerDistanceX(){
        Vector3 vec = player.transform.position - transform.position;
        float dx = Mathf.Abs(vec.x);
        return dx;
    }

    // 仰角を返す
    private Vector2 GetElevationAngle(){
        float v = liquidVelocity;
        float g = 9.8f;
        float d = GetPlayerDistanceX();
        Vector3 vec = player.transform.position - transform.position;
        float y = vec.y;
        Vector2 dir;
        float discriminant = Mathf.Pow(v, 4) - 2 * g * y * Mathf.Pow(v, 2) - Mathf.Pow(g, 2) * Mathf.Pow(d, 2);

        if(discriminant <= 0){
            dir = new Vector2(1 * enemyCtrl.dir, 1);
            
        }else{
            float tan = (Mathf.Pow(v, 2) - Mathf.Sqrt(discriminant)) / (g * d);
            dir = new Vector2(1 * enemyCtrl.dir, tan);
        }

        dir = dir.normalized;
        return dir;
    }

    // 攻撃モーション

    private void ActionAttack(){
        Vector2 dir = GetElevationAngle();

        GameObject liquid = Instantiate(liquidObj, transform.position, transform.rotation);
        liquid.GetComponent<Rigidbody2D>().AddForce(dir * liquidVelocity, ForceMode2D.Impulse);
    }

    // 攻撃の間隔をランダムに指定
    private void SetAttackInterval()
    {
        attackInterval = Random.Range(attackIntervalRange.x, attackIntervalRange.y);
    }
}
