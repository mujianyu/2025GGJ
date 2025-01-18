using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class GhostImage : MonoBehaviour
{
    [Header("残影参数")]
    SpriteRenderer thisSprite;
    // SpriteRenderer playerSprite;
    SpriteRenderer tmpSprite;
    GameObject player;
    Color spriteColor;
    float alpha=0.5f;
    float scale=1;
    float startAlpha;
    // float alphaMultiplyRation = 0.2f;
    [SerializeField] public float activeTime=0.5f;//残影所持续的时间
    float startTime;
 
    //事实上，由于其是单例模式，无需用代码去unity中搜寻该物体，就可以直接从该脚本.instance的方式获取该物体
    //GhostImagePool poolInstance;
 
    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        // playerSprite = player.GetComponent<SpriteRenderer>();
        thisSprite = GetComponent<SpriteRenderer>();
        tmpSprite=thisSprite;
        // thisSprite.sprite = playerSprite.sprite;

        Vector3 offsety=new Vector3(0,0.64f,0);
        transform.position = new Vector3(player.transform.position.x,player.transform.position.y,player.transform.position.z)+offsety;
        transform.rotation = player.transform.rotation;
        transform.localScale = player.transform.localScale;
 
        startTime = Time.time;
    }
    private void Update()
    {
        ImageDisappear();
    }
    void ImageDisappear()
    {
        thisSprite.color = new Color(thisSprite.color.r,thisSprite.color.g , thisSprite.color.b, alpha);
        
        // thisSprite.transform.localScale = new Vector3(thisSprite.transform.localScale.x*scale, thisSprite.transform.localScale.y*scale, thisSprite.transform.localScale.z*scale);
        // scale-=Time.deltaTime * 1 / (activeTime*10);
        // alpha -= Time.deltaTime * 1 / activeTime;//alpha值随时间逐渐减小
    
        if (Time.time > startTime + activeTime)
        {   
            
            // thisSprite.color = new Color(thisSprite.color.r,thisSprite.color.g , thisSprite.color.b, 1);
            GhostImagePool.instance.ReturnPool(this.gameObject);
        }
    }

}