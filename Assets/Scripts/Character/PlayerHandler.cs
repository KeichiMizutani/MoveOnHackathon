using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    // === キャッシュ =========================================
    PlayerController playerCtrl;

    // === コード(MonoBehaviourの基本機能の実装) ================
    void Awake()
    {
        playerCtrl = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (!playerCtrl.activeStatus)
        {
            return;
        }

        float joyMove = Input.GetAxis("Horizontal");
        playerCtrl.ActionMove(joyMove);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerCtrl.ActionJump();
        }
    }
}
