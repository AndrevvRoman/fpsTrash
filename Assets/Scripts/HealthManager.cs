using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    int _health = 3;
    bool _isAlive = true;
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
        if (_isAlive)
        {
            _health--;
            SendMessage("GotDamage");
        }
    }
    public bool Alive()
    {
        return _isAlive;
    }
}
