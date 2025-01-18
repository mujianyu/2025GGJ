using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;


public class move : MonoBehaviour
{
    public SkeletonDataAsset skeletonDataAsset;
    private SkeletonAnimation skeletonAnimation;
    
    [Header("移动参数")]
    private float dirX=0f;
    private float dirY=0f;
    private float speed=7f;
    private Rigidbody2D rb;
    //  private Rigidbody2D rb2D;


    [Header("残影参数")]
 
    [SerializeField] public float dashContinueTime=0.5f;//冲刺持续时间
    [SerializeField] public float dashCD=6f;//冲刺技能的CD
    [SerializeField] public float dashSpeed=7;//冲刺的速度
    // public Image CDImage;
    float dashLeftTime;//剩余可以用来冲刺的时间，初始为其赋值后让时间不断减少
    float lastDashTime=-10f;//上次冲刺的时间，用于设定cd（初始设定为负值是为了初始时也能调用）
    
    public bool isDashing = false;
 
    void CheckIfCanDash()
    {
        
        if (Time.time > lastDashTime + dashCD)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isDashing = true;
                lastDashTime = Time.time;
                dashLeftTime = dashContinueTime;
               
            }
        }
    }
 
    void Dashing()
    {
        if (isDashing)
        {
            if(transform.localScale.x==-1)
                rb.velocity = new Vector2(-dashSpeed, rb.velocity.y);
            else if (transform.localScale.x == 1)
                rb.velocity = new Vector2(dashSpeed, rb.velocity.y);

            dashLeftTime -= Time.deltaTime;
            if(dashLeftTime <= 0)
            {
                isDashing = false;
            }
            GhostImagePool.instance.GetGhostImageFromPool();
    
        }else rb.velocity = new Vector2(0.0f,0.0f);
    }



    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        skeletonAnimation.skeletonDataAsset = skeletonDataAsset;
        skeletonAnimation.Initialize(true);
        skeletonAnimation.AnimationState.SetAnimation(0, PlayerState.Idle, true);
        rb = GetComponent<Rigidbody2D>();
    }
 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            skeletonAnimation.AnimationState.SetAnimation(0, PlayerState.Walk, true);
        }

        CheckIfCanDash();
        Dashing();

    }

}
