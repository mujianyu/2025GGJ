using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
//用于管理状态机切换
public class StateMachina : MonoBehaviour
{
    IState currentState;
 
 
    private void Update()
    {
        currentState?.LogicUpdata();
    }
    private void FixedUpdate()
    {
        currentState?.PhysicUpdata();
    }
    public virtual void AnimationEvent()
    {
        currentState?.AinamtionEvent();
    }
 
    public virtual void SwitchState(IState newState)
    {
        currentState?.Exit();
        currentState=newState;
        currentState.Enter();
    }
 
}