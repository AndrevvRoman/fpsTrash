using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 10f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.05f;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;
    public State currentState;
    void GroundCheck()
    {
        var curCheck = Physics.CheckSphere(groundCheck.position,groundDistance,groundMask);
        if (isGrounded != curCheck)
        {
            if(curCheck == true)
            {
                currentState = State.Grounded;
            }
            else
            {
                currentState = State.Jumping;
            }
            isGrounded = curCheck;
        }
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -0.5f;
        }
        if (velocity.y < -0.8f)
        {
            currentState = State.Jumping;
        }

    }
    public State GetState()
    {
        return currentState;
    }
    public bool Grounded()
    {
        return isGrounded;
    }
    public void UpdateMovment(float x,float z)
    {
        currentState = State.Idle;
        
        Vector3 move = transform.right * x + transform.forward * z;

        if(move != Vector3.zero && !GetComponent<AtackManager>().isAtacking() && !GetComponent<AtackManager>().isBlocking())
        {   
            if (!GetComponent<WeaponManager>().isArmed())
            {
                controller.Move(move * speed * Time.deltaTime);
                if (currentState != State.Jumping)
                    currentState = State.Running;
            }
            else 
            {
                controller.Move(move * speed / 2 * Time.deltaTime);
                if (currentState != State.Jumping)
                    currentState = State.Walking;
            }
            
        }
        GroundCheck();
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * (-2f) * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        if (velocity.y < -19f)
        {
            velocity.y = -19f;
        } 
        controller.Move(velocity * Time.deltaTime);
    }
}
