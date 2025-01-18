/*
 * @FilePath: \undefinedc:\Users\25594\Desktop\GGJ2025\Assets\Scripts\SpineAnimationController.cs
 * @Description:  
 * @Author: MuJianYu
 * @Version: 0.0.1
 * @LastEditors: MuJianYu
 * @LastEditTime: 2025-01-05 21:11:25
 * Copyright    : G AUTOMOBILE RESEARCH INSTITUTE CO.,LTD Copyright (c) 2025.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
 

public class SpineAnimationController : MonoBehaviour
{

    public SkeletonDataAsset skeletonDataAsset;
    private SkeletonAnimation skeletonAnimation;
    public string animationName;
 
    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        skeletonAnimation.skeletonDataAsset = skeletonDataAsset;
        skeletonAnimation.Initialize(true);
    }
 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            skeletonAnimation.AnimationState.SetAnimation(0, animationName, true);
        }
    }
}