using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HealthManager : NetworkBehaviour
{
    int _health = 3;
    bool _isAlive = true;
    AtackManager _attackManager;
    void Update()
    {
        if (_health == 0 && _isAlive)
        {
            _isAlive = false;
            StartCoroutine(_Die());
        }
    }
    IEnumerator _Die()
    {
        Debug.Log("Im dead");
        SendMessage("Die");
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
    public void GetDamage()
    {
        CmdHandleDamage();
    }
    public bool Alive()
    {
        return _isAlive;
    }

    public delegate void GetDamageDelegate();

    [SyncEvent]
    public event GetDamageDelegate EventGetDamage;

    #region Server

    [Server]
    private void HandleDamage()
    {
        if (_isAlive)
        {
            if (!GetComponent<AtackManager>().isBlocking())
            {
                _health--;
                SendMessage("GotDamage");
            }
            else
            {
                SendMessage("BlockedDamage");
            }
        }
        EventGetDamage?.Invoke();
    }

    [Command]
    private void CmdHandleDamage() => HandleDamage();
    #endregion
}
