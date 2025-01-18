using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(RectTransform))]
public class UIState : MonoBehaviour,IState
{
 
    protected Canvas canvas;
    protected CanvasGroup canvasGroup;
    protected RectTransform rectTransform;//ui位置
 
    protected int initialSortingOrder;
 
 
    protected virtual void Awake()
    {
 
        canvas = GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform= GetComponent<RectTransform>();
        initialSortingOrder = canvas.sortingOrder;
    }
 
 
    public virtual void AinamtionEvent()
    {
        
    }
 
    public virtual void Enter()
    {
        canvas.enabled = true;
    }
 
    public virtual void Exit()
    {
      canvas.enabled = false;
    }
 
    public virtual void LogicUpdata()
    {
      
    }
 
    public virtual void PhysicUpdata()
    {
       
    }
}