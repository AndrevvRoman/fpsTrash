using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int m_health = 3;
    void Update()
    {
        if (m_health == 0)
        {
            Debug.Log("Im dead");
            Destroy(this.gameObject);
        }
    }
    public void GetDamage()
    {
        m_health--;
        SendMessage("GotDamage");
    }
}
