using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HealthManager : NetworkBehaviour
{
   [SyncVar] int _health = 3;
   [SyncVar] bool _isAlive = true;
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
        _health = health;
        SendMessage("GetDamage");
    }
    public void OnAliveChanged(bool alive)
    {
        _isAlive = alive;
        //SendMessage("GetDamage");
    }


    [Command]
    void CmdHandleDamage()
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
}
