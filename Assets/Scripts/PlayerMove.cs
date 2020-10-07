using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerMove : NetworkBehaviour
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
    [SyncVar] public State currentState;
    void GroundCheck()
    {
        var curCheck = Physics.CheckSphere(groundCheck.position,groundDistance,groundMask);
        if (isGrounded != curCheck)
        {
            if(curCheck == true)
            {
                CmdSetState(State.Grounded);
            }
            else
            {
                CmdSetState(State.Jumping);
            }
            isGrounded = curCheck;
        }
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -0.5f;
        }
        if (velocity.y < -0.8f)
        {
            CmdSetState(State.Jumping);
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
        CmdSetState(State.Idle);
        
        Vector3 move = transform.right * x + transform.forward * z;

        if(move != Vector3.zero && !GetComponent<AtackManager>().isAtacking() && !GetComponent<AtackManager>().isBlocking())
        {   
            if (!GetComponent<WeaponManager>().isArmed())
            {
                controller.Move(move * speed * Time.deltaTime);
                if (currentState != State.Jumping)
                    CmdSetState(State.Running);
            }
            else 
            {
                controller.Move(move * speed / 2 * Time.deltaTime);
                if (currentState != State.Jumping)
                    CmdSetState(State.Walking);
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

    [Command]
    protected void CmdSetState(State state)
    {
        currentState = state;
    }
}
