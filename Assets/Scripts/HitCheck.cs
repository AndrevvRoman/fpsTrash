using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCheck : MonoBehaviour
{
    AtackManager _atackManager;
    void Start()
    {
        _atackManager = GetComponentInParent<AtackManager>();
    }
    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Robot")
        {
            _atackManager.MakeHit(collision.gameObject);
        }
    }
}
