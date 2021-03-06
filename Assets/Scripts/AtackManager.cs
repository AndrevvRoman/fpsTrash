﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AtackManager : NetworkBehaviour
{
    WeaponManager _weaponManager;
    bool _isAttacking = false;
    bool _isBlocking = false;

    void Start()
    {
        _weaponManager = GetComponent<WeaponManager>();
    }

    public void UpdateAtack()
    {
        if (Input.GetButtonDown("Fire1") && _weaponManager.isArmed() && !_isBlocking)
        {
            _isAttacking = true;
            SendMessage("StartAttacking");
        }
        if (Input.GetButtonUp("Fire1"))
        {
            _isAttacking = false;
            SendMessage("StopAttacking");
        }

        if (Input.GetButtonDown("Fire2") && _weaponManager.isArmed() && !_isAttacking)
        {
            _isBlocking = true;
            SendMessage("StartBlocking");
        }
        if (Input.GetButtonUp("Fire2"))
        {
            _isBlocking = false;
            SendMessage("StopBlocking");
        }
    }

    public bool isAtacking()
    {
        return _isAttacking;
    }
    public bool isBlocking()
    {
        return _isBlocking;
    }
    public void MakeHit(GameObject target)
    {
        if (_isAttacking)
        {
            //Debug.Log("Sent CMD hit");
            CmdSendHitToTarget(target);
        }
    }

    [Command]
    void CmdSendHitToTarget(GameObject target)
    {
        //Debug.Log("Handle CMD hit");
        target.SendMessage("GetDamage");
    }
}
