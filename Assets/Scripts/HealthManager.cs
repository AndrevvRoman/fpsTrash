using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HealthManager : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnHealthChanged))] int _health = 3;
    [SyncVar] bool _isAlive = true;
    bool _isAttacked = false;
    AtackManager _attackManager;

    void Update()
    {
        if (_isAttacked)
        {
            Debug.Log("Sending cmd");
            CmdHandleDamage();
            _isAttacked = false;
        }
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
        _isAttacked = true;
    }

    public bool Alive()
    {
        return _isAlive;
    }

    public void OnHealthChanged(int oldHealth, int newHealth)
    {
        _health = newHealth;
        SendMessage("GetDamage");
    }

    public void OnAliveChanged(bool alive)
    {
        _isAlive = alive;
        //SendMessage("GetDamage");
    }

    
    void CmdHandleDamage()
    {
        Debug.Log("HandlingDamage");
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


}
