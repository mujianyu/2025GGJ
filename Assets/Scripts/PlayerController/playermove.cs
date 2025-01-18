using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class playermove : MonoBehaviour
{
    [Header("移动参数")]
    public GameObject model;//用子物体作为移动的方向
    public PlayerInput pi;
    public float walkSpeed = 600f;
    public float runMultiplier = 2.7f;
    private bool isRight = true;
 
    [Header("技能参数")]
    [SerializeField]
    private Rigidbody2D rigid;
    private Vector3 movingVec;


    
	// Use this for initialization
	void Awake () {
        // spineManage=GetComponent<SpineManage>();
        pi = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody2D>();
        // skeletonAnimation = GetComponent<SkeletonAnimation>();
        // skeletonAnimation.skeletonDataAsset = skeletonDataAsset;
        // skeletonAnimation.Initialize(true);

        //播放待机动画
        
	}
	
	// Update is called once per frame
	void Update () {
        
        float targetRunMulti = (pi.run ? 2.0f : 1.0f);
        
        if (pi.Dmag > 0.1f)
        {
            //移动的值>0.1f时，才进行旋转
            Vector3 targetUp = Vector3.Slerp(model.transform.up, pi.Dvec, 0.4f);  //插值 移动的方向和物体的朝向进行插值计算
            //改变子物体的朝向
            model.transform.up = targetUp;      
        }
        //使用子物体的方向作为移动的方向 pi.Dmag是移动的速度 model.transform.up是移动的方向
        movingVec = pi.Dmag * model.transform.up * walkSpeed * (pi.run ? runMultiplier : 1.0f);
        checkChageDir();
        Vector3 scale = transform.localScale;
        
        if(isRight){
            scale.x = Mathf.Abs(scale.x);
        }else {
            scale.x = -Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
        
	}
    private void checkChageDir(){
        if (model.transform.up.x > 0.0f)
        {
            isRight = true;
        }else isRight = false;
    } 
 
    private void FixedUpdate()
    {
        rigid.velocity = new Vector3(movingVec.x, movingVec.y, 0);   //用刚体来进行移动
        

        // skeletonAnimation.AnimationState.SetAnimation(0, animationName, true);
    }



}
