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
    private ScoreManager m_scoreManager;

    void Start()
    {
        _health = maxHealth;
        m_scoreManager = GameObject.FindGameObjectWithTag("ScoreBoard").GetComponent<ScoreManager>();
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
    }

    public void OnAliveChanged(bool oldAlive, bool newAlive)
    {
        _isAlive = newAlive;
        if (oldAlive == true)
        {
            StartCoroutine(_Die());
        }
        else
        {
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
        m_scoreManager.AddScore(GetComponent<PlayerTeam>().GetTeam());
        RpcRespawn(GetComponent<PlayerTeam>().startPos.position);   
    }

    [ClientRpc]
    void RpcRespawn(Vector3 spawnPoint)
    {
        if (isLocalPlayer)
        {
            transform.position = spawnPoint;
            SendMessage("BackToLive");
        }
    }

}
