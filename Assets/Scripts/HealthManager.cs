using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HealthManager : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnHealthChanged))] int _health = 2;
    [SyncVar(hook = nameof(OnAliveChanged))] bool _isAlive = true;
    bool _isAttacked = false;
    AtackManager _attackManager;

    void Update()
    {
        if (_isAttacked)
        {
            _isAttacked = false;
            TakeDamage();
        }
        if (_health == 0 && _isAlive)
        {
            _isAttacked = false;
            CmdDie();
        }
    }

    IEnumerator _Die()
    {
        SendMessage("Die");
        yield return new WaitForSeconds(3f);
        //NetworkServer.Destroy(this.gameObject);
    }

    void TakeDamage()
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
    }

    public void GetDamage()
    {
        _isAttacked = true;
    }

    public bool Alive()
    {
        return _isAlive;
    }

    public void OnHealthChanged(int oldHealth, int newHealth)
    {
        //Debug.Log("Handling Health Change");
        _health = newHealth;
        SendMessage("GotDamage");  
    }

    public void OnAliveChanged(bool oldAlive, bool newAlive)
    {
        Debug.Log("Handling Alive Change");
        _isAlive = newAlive;
        StartCoroutine(_Die());
    }

    [Command]
    void CmdDie()
    {
        _isAlive = false;
        StartCoroutine(_Die());
    }


}
