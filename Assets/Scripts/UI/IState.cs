using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public interface IState//interface 状态机接口
{
    void Enter();
    void Exit();
    void LogicUpdata();
    void PhysicUpdata();
    void AinamtionEvent();
}