using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControlManager : MonoBehaviour
{
    PlayerMove _mover;
    AnimationManager _animManager;
    WeaponManager _weaponManager;
    MouseLook _mouseLookManager;
    void Start()
    {
        _mover = GetComponent<PlayerMove>();
        _animManager = GetComponent<AnimationManager>();
        _weaponManager = GetComponent<WeaponManager>();
        _mouseLookManager = GetComponentInChildren<MouseLook>();   
    }
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        _mover.UpdateMovment(x,z);

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        _mouseLookManager.UpdateLook(mouseX,mouseY);
        _weaponManager.UpdateWeapon();
    }
}
