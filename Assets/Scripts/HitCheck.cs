using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCheck : MonoBehaviour
{
    AtackManager _atackManager;
    float m_lastHitTime = 0f;
    float m_hitDelay = 0.5f;
    void Start()
    {
        m_lastHitTime = Time.time;
        _atackManager = GetComponentInParent<AtackManager>();
    }
    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Robot" && Time.time - m_lastHitTime > m_hitDelay)
        {   
            m_lastHitTime = Time.time;
            _atackManager.MakeHit(collision.gameObject);
        }
    }
}
