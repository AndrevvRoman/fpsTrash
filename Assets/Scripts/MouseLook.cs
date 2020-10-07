using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class MouseLook : MonoBehaviour
{
    float xRotation = 0;
    public float mouseSens = 100f;
    public bool isCursorLocked = true;
    public Transform playerBody;
    public float minimalAngle = -60f;
    public float maxmimalAngle = 60f;
    public void UpdateLook(float mouseX,float mouseY)
    {
        if (isCursorLocked)
            Cursor.lockState = CursorLockMode.Locked;
        
        mouseX = mouseX * mouseSens * Time.deltaTime;
        mouseY = mouseY * mouseSens * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation,minimalAngle,maxmimalAngle);

        transform.localRotation = Quaternion.Euler(xRotation,0f,0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

}