using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class InputControlManager : NetworkBehaviour
{
    PlayerMove _mover;
    AnimationManager _animManager;
    WeaponManager _weaponManager;
    MouseLook _mouseLookManager;
    AtackManager _atackManager;
    Camera _cam;
    void Start()
    {
        _mover = GetComponent<PlayerMove>();
        _animManager = GetComponent<AnimationManager>();
        _weaponManager = GetComponent<WeaponManager>();
        _mouseLookManager = GetComponentInChildren<MouseLook>();
        _atackManager = GetComponent<AtackManager>();

        _cam = GetComponentInChildren<Camera>();
    }
    void Update()
    {
        if (!isLocalPlayer)
        {
            _cam.enabled = false;
            return;
        }
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        _mover.UpdateMovment(x,z);

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        _mouseLookManager.UpdateLook(mouseX,mouseY);
        _weaponManager.UpdateWeapon();
        _atackManager.UpdateAtack();
    }
}
