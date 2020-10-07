using System.Collections;
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
            CmdChangeArmed();
        }
    }
    public bool isArmed()
    {
        return _isArmed;
    }

    public delegate void StateChangedDelegate(bool isArmed);

    [SyncEvent]
    public event StateChangedDelegate EventStateChanged;

    #region Server

    [Server]
    private void SetStates()
    {
        Debug.Log("In setStates");
        _isArmed = !_isArmed;
        _weapon.SetActive(_isArmed);
        SendMessage("SwitchArmed");
        EventStateChanged?.Invoke(_isArmed);
    }

    [Command]
    private void CmdChangeArmed() => SetStates();
    #endregion
}
