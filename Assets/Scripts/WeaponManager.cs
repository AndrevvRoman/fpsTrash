﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponManager : NetworkBehaviour
{    
    [SyncVar] bool _isArmed = false;
    GameObject _weapon;
    void Start()
    {
        _weapon = GameObject.FindWithTag("Weapon");
        _weapon.SetActive(_isArmed);
    }
    public void UpdateWeapon()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            _isArmed = !_isArmed;
            _weapon.SetActive(_isArmed);
            SendMessage("SwitchArmed");
        }
    }
    public bool isArmed()
    {
        return _isArmed;
    }
}
