using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HealthManager : NetworkBehaviour
{
   [SyncVar(hook = "OnHealthChanged")] int _health = 3;
   [SyncVar(hook = "OnAliveChanged")] bool _isAlive = true;
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
        //if (_isAlive)
        //{
        //    if (!GetComponent<AtackManager>().isBlocking())
        //    {
        //        //_health--;
        //        SendMessage("GotDamage");
        //    }
        //    else
        //    {
        //        SendMessage("BlockedDamage");
        //    }
        //}
        CmdHandleDamage();
    }

    public bool Alive()
    {
        return _isAlive;
    }

    public void OnHealthChanged(int health)
    {
        Debug.Log("InHealthChanged");
        _health = health;
        SendMessage("GetDamage");
    }
    public void OnAliveChanged(bool alive)
    {
        Debug.Log("InHealthChanged");
        _isAlive = alive;
        //SendMessage("GetDamage");
    }


    [Command]
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
