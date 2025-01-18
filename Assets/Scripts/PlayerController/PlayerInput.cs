using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 
public class PlayerInput : MonoBehaviour {
 
    [Header("===== Keys settings  =====")]
    public string keyUp = "w";
    public string keyDown = "s";
    public string keyLeft = "a";
    public string keyRight = "d";
 
    public string keyA = "left shift";
    public string keyB;
    public string keyC;
    public string keyD;
 
    [Header("===== Output signals =====")]
    public float Dup;      //上下信号
    public float Dright;   //左右信号
    public float Dmag;      //速度
    public Vector3 Dvec;    //旋转
 
    public bool run;        //跑步信号
    public bool tmprun;        //跑步信号
 
    [Header("===== Others =====")]
    public bool inputEnable = true;     //输入开关
 
    private float targetDup;
    private float targetDright;
    private float velocityDup;
    private float velocityDright;
    
    [Header("残影参数")]
 
    [SerializeField] public float dashContinueTime=0.5f;//冲刺持续时间
    [SerializeField] public float dashCD=2f;//冲刺技能的CD
    [SerializeField] public float dashSpeed=7;//冲刺的速度
    [SerializeField] public float spawdash=0.05f;//冲刺的速度

    // [Header("动画参数")]
    // public SkeletonDataAsset skeletonDataAsset;
    // private SkeletonAnimation skeletonAnimation;
    // public string animationName;

    // [Header("SpineManage")]
    // private SpineManage  spineManage;

     private Animator anim;
    
    // public Image CDImage;
    float dashLeftTime;//剩余可以用来冲刺的时间，初始为其赋值后让时间不断减少
    float lastDashTime=-10f;//上次冲刺的时间，用于设定cd（初始设定为负值是为了初始时也能调用）

    float lastSpawndashTime=-10;

    public bool isDashing = false;

    

    
	// Use this for initialization
	void Awake () {
       anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        targetDup = (Input.GetKey(keyUp) ? 1.0f : 0) - (Input.GetKey(keyDown) ? 1.0f : 0);//防止前后一起按住
        targetDright = (Input.GetKey(keyRight) ? 1.0f : 0) - (Input.GetKey(keyLeft) ? 1.0f : 0);//防止左右一起按照1
 
        if (inputEnable == false)
        {
            targetDup = 0;
            targetDright = 0;
        }

        
        //Dup存储了前后当前的速度状态，还为下一帧的计算提供了必要的信息
        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
        //Dright存储了左右当前的速度状态，还为下一帧的计算提供了必要的信息
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);

        //将平面坐标转换为圆形坐标
        Vector2 tempDAxis = SquareToCircle(new Vector2(Dright, Dup));


        float Dright2 = tempDAxis.x;
        float Dup2 = tempDAxis.y;
      

        Dmag = Mathf.Sqrt((Dup2 * Dup2) + (Dright2 * Dright2));
        //移动
        if(Dmag<0.1f)
        {
            anim.SetBool("walk", false);
        }else {
            anim.SetBool("walk", true);
        }
        Dvec = Dright2 * transform.right + Dup2 * transform.up;

        
        CheckIfCanDash();
        Dashing();
    }
    void Dashing()
    {
        if (isDashing)
        {
            dashLeftTime -= Time.deltaTime;
            if(dashLeftTime <= 0)
            {
                isDashing = false;
            }
            run=true;
            if(Time.time>lastSpawndashTime+spawdash)
            {
                lastSpawndashTime=Time.time;
                GhostImagePool.instance.GetGhostImageFromPool();
            }
            anim.SetBool("run", true);
            // GhostImagePool.instance.GetGhostImageFromPool();
        }else {
            run=false;
            anim.SetBool("run", false);

        }
    }
    void CheckIfCanDash()
    {
        
        if (Time.time > lastDashTime + dashCD)
        {
            // tmprun = Input.GetKey(keyA);
            if (Input.GetKey(keyA))
            {
                isDashing = true;
                lastDashTime = Time.time;
                dashLeftTime = dashContinueTime;
               
            }
        }
    }
 
    /// <summary>
    /// 平面坐标转圆形坐标
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);
 
        return output;
    }

}
