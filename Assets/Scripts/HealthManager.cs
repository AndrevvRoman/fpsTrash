using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HealthManager : NetworkBehaviour
{
    public int maxHealth = 3;
    [SyncVar(hook = nameof(OnHealthChanged))] int _health;
    [SyncVar(hook = nameof(OnAliveChanged))] bool _isAlive = true;
    bool _isAttacked = false;
    AtackManager _attackManager;

    void Start()
    {
        _health = maxHealth;
    }

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
        CmdRespawn();
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
        _health = newHealth;
        if (oldHealth > 0)
        {
            SendMessage("GotDamage");
        }
        else
        {
            Debug.Log("Looks like respawn in health");
        }
    }

    public void OnAliveChanged(bool oldAlive, bool newAlive)
    {
        _isAlive = newAlive;
        if (oldAlive == true)
        {
            Debug.Log("Handling Alive Change");
            StartCoroutine(_Die());
        }
        else
        {
            Debug.Log("Looks like respawn in alive");
            SendMessage("BackToLive");
        }
    }

    [Command]
    void CmdDie()
    {
        _isAlive = false;
        StartCoroutine(_Die());
    }

    [Command]
    void CmdRespawn()
    {
        _isAlive = true;
        _health = maxHealth;
        RpcRespawn();   
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            Debug.Log("Respawning in process");
            Vector3 spawnpoint = Vector3.zero;
            transform.position = spawnpoint;
            SendMessage("BackToLive");
        }
    }

}
