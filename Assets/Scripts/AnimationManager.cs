using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AnimationManager : NetworkBehaviour
{
    Animator animanor;
    PlayerMove playerMover;
    State lastKnownState;
    HealthManager m_healthManager;
    float timeSinceLastTrigger;
    [SyncVar] bool _isArmed = false;
    [SyncVar] bool _isAttacking = false;
    [SyncVar] bool _isBlocking = false;

    void Start()
    {
        animanor = GetComponent<Animator>();
        playerMover = GetComponent<PlayerMove>();
        m_healthManager = GetComponent<HealthManager>();
        lastKnownState = playerMover.GetState();
    }
    void UpdateUnArmed(State state)
    {
        switch (state)
        {
            case State.Idle:
                animanor.SetBool("isIdle", true);
                animanor.SetBool("isRunning", false);
                animanor.SetBool("isJumping", false);
                animanor.SetBool("hasGrounded", false);
                break;
            case State.Running:
                animanor.SetBool("isIdle", false);
                animanor.SetBool("isRunning", true);
                break;
            case State.Jumping:
                animanor.SetBool("isIdle", false);
                animanor.SetBool("isJumping", true);
                animanor.SetBool("isRunning", false);
                animanor.SetBool("hasGrounded", false);
                break;
            case State.Grounded:
                animanor.SetBool("isIdle", false);
                animanor.SetBool("hasGrounded", true);
                animanor.SetBool("isJumping", false);
                break;
        }
    }
    void UpdateArmed(State state)
    {
        if (_isAttacking)
        {
            animanor.SetBool("isAttacking", true);
        }
        else
        {
            animanor.SetBool("isAttacking", false);
        }
        if (_isBlocking)
        {
            animanor.SetBool("isBlocking", true);
        }
        else
        {
            animanor.SetBool("isBlocking", false);
        }
        switch (state)
        {
            case State.Walking:
                animanor.SetBool("isWalking", true);
                animanor.SetBool("isIdle", false);
                break;
            case State.Idle:
                animanor.SetBool("isIdle", true);
                animanor.SetBool("isWalking", false);
                break;
            case State.Jumping:
                animanor.SetBool("isIdle", false);
                animanor.SetBool("isJumping", true);
                animanor.SetBool("isRunning", false);
                animanor.SetBool("hasGrounded", false);
                break;
            case State.Grounded:
                animanor.SetBool("isIdle", false);
                animanor.SetBool("hasGrounded", true);
                animanor.SetBool("isJumping", false);
                break;
        }
    }
    void UpdateAnimation(State state)
    {
        animanor.SetBool("isArmed", _isArmed);
        switch (_isArmed)
        {
            case false:
                UpdateUnArmed(state);
                break;
            case true:
                UpdateArmed(state);
                break;
        }
    }
    [ClientCallback]
    void Update()
    {
        if(!hasAuthority) { return; }
        
        var curState = playerMover.GetState();
        lastKnownState = curState;
        //CmdChangeState(_isArmed, _isAttacking, _isBlocking);
        if (m_healthManager.Alive())
            UpdateAnimation(lastKnownState);
        
    }
    public void SwitchArmed()
    {
        _isArmed = !_isArmed;
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
    public void BlockedDamage()
    {
        animanor.SetTrigger("BlockedDamage");
    }
    public void Die()
    {
        animanor.SetTrigger("Die");
    }
    public void BackToLive()
    {
        animanor.SetTrigger("BackToLive");
    }


    //public delegate void StateChangedDelegate(bool isArmed, bool isAttacking, bool isBlocking);

    //[SyncEvent]
    //public event StateChangedDelegate EventStateChanged;

    //#region Server

    //[Server]
    //private void SetStates(bool isArmed, bool isAttacking, bool isBlocking)
    //{
    //    Debug.Log("In setStates");
    //    _isArmed = isArmed;
    //    _isAttacking = isAttacking;
    //    _isBlocking = isBlocking;
    //    EventStateChanged?.Invoke(_isArmed, _isAttacking, _isBlocking);
    //}

    //public override void OnStartServer() => SetStates(false, false, false);

    //[Command]
    //private void CmdChangeState(bool _1, bool _2, bool _3) => SetStates(_1, _2, _3);
    //#endregion
}
