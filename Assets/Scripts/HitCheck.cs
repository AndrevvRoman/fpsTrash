using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HitCheck : NetworkBehaviour
{
    AtackManager _atackManager;
    float m_lastHitTime = 0f;
    float m_hitDelay = 0.5f;
    void Start()
    {
        m_lastHitTime = Time.time;
        _atackManager = GetComponentInParent<AtackManager>();
    }
    void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.tag == "Robot" && Time.time - m_lastHitTime > m_hitDelay)
        {
            //Debug.Log("this = " + this.gameObject);
            //Debug.Log("Coll = " + collision.gameObject.GetInstanceID());
            m_lastHitTime = Time.time;
            _atackManager.MakeHit(collision.gameObject);
        }
        //Physics.IgnoreCollision(collision.collider, GetComponent<Collider>(),false);
    }
}
