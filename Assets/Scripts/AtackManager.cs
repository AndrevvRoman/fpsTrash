using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackManager : MonoBehaviour
{
    WeaponManager _weaponManager;
    bool _isAttacking = false;
    bool _isBlocking = false;

    void Start()
    {
        _weaponManager = GetComponent<WeaponManager>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1") && _weaponManager.isArmed() && !_isBlocking)
        {
            _isAttacking = true;
            SendMessage("StartAttacking");
        }
        if(Input.GetButtonUp("Fire1"))
        {
            _isAttacking = false;
            SendMessage("StopAttacking");
        }

        if(Input.GetButtonDown("Fire2") && _weaponManager.isArmed() && !_isAttacking)
        {
            _isBlocking = true;
            SendMessage("StartBlocking");
        }
        if(Input.GetButtonUp("Fire2"))
        {
            _isBlocking = false;
            SendMessage("StopBlocking");
            Debug.Log("released");
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
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Robot")
        {
            Debug.Log("Attacking");
        }
    }
    public void MakeHit(GameObject target)
    {
        if(_isAttacking)
        {
            target.SendMessage("GetDamage");
            Debug.Log("Make Hit");
        }
    }
}
