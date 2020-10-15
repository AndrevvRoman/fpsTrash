using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponManager : NetworkBehaviour
{    
    [SyncVar(hook = "OnArmedChanged")] bool _isArmed = false;
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
            CmdChangeArmed();
        }
    }
    public bool isArmed()
    {
        return _isArmed;
    }

    public void OnArmedChanged(bool isArmed)
    {
        _isArmed = isArmed;
        _weapon.SetActive(_isArmed);
    }

    public void ChangeArmed()
    {
        Debug.Log("In setArmed");
        _isArmed = !_isArmed;
        _weapon.SetActive(_isArmed);
        SendMessage("SwitchArmed");
    }

    [Command]
    void CmdChangeArmed()
    {
        ChangeArmed();
    }
}
