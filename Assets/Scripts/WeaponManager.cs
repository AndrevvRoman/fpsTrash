using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{    
    bool _isArmed = false;
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
