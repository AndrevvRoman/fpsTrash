using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WeaponManager : NetworkBehaviour
{
    [SyncVar(hook =nameof(OnArmedChanged))] bool _isArmed = false;
    public List<GameObject> _weapons;
    void Start()
    {
        foreach(GameObject obj in _weapons)
        {
            obj.SetActive(_isArmed);
        }
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
        foreach (GameObject obj in _weapons)
        {
            obj.SetActive(_isArmed);
        }
    }

    public void ChangeArmed()
    {
        _isArmed = !_isArmed;
        foreach (GameObject obj in _weapons)
        {
            obj.SetActive(_isArmed);
        }
        SendMessage("SwitchArmed");
    }

    #region ServerCommands
    [Command]
    void CmdChangeArmed()
    {
        _isArmed = !_isArmed;
        foreach (GameObject obj in _weapons)
        {
            obj.SetActive(_isArmed);
        }
        SendMessage("SwitchArmed");
    }
    #endregion

    #region ClientRPC
    [TargetRpc]
    void TargetChangeArmed()
    {
        foreach (GameObject obj in _weapons)
        {
            obj.SetActive(_isArmed);
        }
    }
    #endregion

}
