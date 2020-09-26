using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    Animator animanor;
    PlayerMove playerMover;
    State lastKnownState;
    HealthManager m_healthManager;
    float timeSinceLastTrigger;
    public bool isArmed = true;
    bool _isAttacking = false;
    bool _isBlocking = false;
    void Start()
    {
        animanor = GetComponentInChildren<Animator>();
        playerMover = GetComponent<PlayerMove>();
        m_healthManager = GetComponent<HealthManager>();
        lastKnownState = playerMover.GetState();
    }
    void UpdateUnArmed(State state)
    {
        switch(state)
        {
            case State.Idle:
            animanor.SetBool("isIdle",true);
            animanor.SetBool("isRunning",false);
            animanor.SetBool("isJumping",false);
            animanor.SetBool("hasGrounded",false);
            break;
            case State.Running:
            animanor.SetBool("isIdle",false);
            animanor.SetBool("isRunning",true);
            break;
            case State.Jumping:
            animanor.SetBool("isIdle",false);
            animanor.SetBool("isJumping",true);
            animanor.SetBool("isRunning",false);
            animanor.SetBool("hasGrounded",false);
            break;
            case State.Grounded:
            animanor.SetBool("isIdle",false);
            animanor.SetBool("hasGrounded",true);
            animanor.SetBool("isJumping",false);
            break;
        }
    }
    void UpdateArmed(State state)
    {
        if (_isAttacking)
        {
            animanor.SetBool("isAttacking",true);
        }
        else
        {
            animanor.SetBool("isAttacking",false);
        }
        if (_isBlocking)
        {
            animanor.SetBool("isBlocking",true);
        }
        else
        {
            animanor.SetBool("isBlocking",false);
        }
        switch(state)
        {
            case State.Walking:
            animanor.SetBool("isWalking",true);
            animanor.SetBool("isIdle",false);
            break;
            case State.Idle:
            animanor.SetBool("isIdle",true);
            animanor.SetBool("isWalking",false);
            break;
            case State.Jumping:
            animanor.SetBool("isIdle",false);
            animanor.SetBool("isJumping",true);
            animanor.SetBool("isRunning",false);
            animanor.SetBool("hasGrounded",false);
            break;
            case State.Grounded:
            animanor.SetBool("isIdle",false);
            animanor.SetBool("hasGrounded",true);
            animanor.SetBool("isJumping",false);
            break;
        }
    }
    void UpdateAnimation(State state)
    {
        animanor.SetBool("isArmed",isArmed);
        switch(isArmed)
        {
            case false:
            UpdateUnArmed(state);
            break;
            case true:
            UpdateArmed(state);
            break;
        }
    }
    void Update()
    {
        var curState = playerMover.GetState();
        lastKnownState = curState;
        if(m_healthManager.Alive())
            UpdateAnimation(lastKnownState);
    }
    public void SwitchArmed()
    {
        isArmed = !isArmed;
    }
    public void StartAttacking()
    {
        _isAttacking = true;
    }
    public void StopAttacking()
    {
        _isAttacking = false;
    }

    public void StartBlocking()
    {
        _isBlocking = true;
    }
    public void StopBlocking()
    {
        _isBlocking = false;
    }

    public void GotDamage()
    {
        animanor.SetTrigger("GotDamage");
    }

    public void Die()
    {
        animanor.SetTrigger("Die");
    }
}
