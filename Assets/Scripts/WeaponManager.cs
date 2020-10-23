using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WeaponManager : NetworkBehaviour
{
    [SyncVar(hook =nameof(OnArmedChanged))] bool _isArmed = false;
    public GameObject _weapon;
    void Start()
    {
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

    public void OnArmedChanged(bool oldValue, bool newValue)
    {
        _isArmed = newValue;
        _weapon.SetActive(_isArmed);
    }

    public void ChangeArmed()
    {
        _isArmed = !_isArmed;
        _weapon.SetActive(_isArmed);
        SendMessage("SwitchArmed");
    }

    #region ServerCommands
    [Command]
    void CmdChangeArmed()
    {
        _isArmed = !_isArmed;
        _weapon.SetActive(_isArmed);
        SendMessage("SwitchArmed");
    }
    #endregion

    #region ClientRPC
    [TargetRpc]
    void TargetChangeArmed()
    {
        _weapon.SetActive(_isArmed);
    }
    #endregion

}
