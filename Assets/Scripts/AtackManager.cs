using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackManager : MonoBehaviour
{
    WeaponManager _weaponManager;
    bool _isAttacking = false;

    void Start()
    {
        _weaponManager = GetComponent<WeaponManager>();

    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1") && _weaponManager.isArmed())
        {
            _isAttacking = true;
            SendMessage("StartAttacking");
        }
        if(Input.GetButtonUp("Fire1"))
        {
            _isAttacking = false;
            SendMessage("StopAttacking");
        }
    }

    public bool isAtacking()
    {
        return _isAttacking;
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
