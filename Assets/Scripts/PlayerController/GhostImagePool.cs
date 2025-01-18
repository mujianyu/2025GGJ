using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class GhostImagePool : MonoBehaviour
{
    public static GhostImagePool instance;
    int ghostImageGroupCount = 10;//对象池中的影子不够用时会多生成一组影子，这是一组影子的数量
    public GameObject ghostImagePrefab;
 
    void Awake()
    {
        instance = this;
    }
 
    Queue<GameObject> pool=new Queue<GameObject>();
    private void Start()
    {
        FillPool();
    }
    void Update()
    {
        
    }
 
    void FillPool()
    {
        for(int i = 0; i < ghostImageGroupCount; i++)
        {
            var ghostImage = Instantiate(ghostImagePrefab, transform);
            ReturnPool(ghostImage);
        }
 
    }
 
    public void ReturnPool(GameObject ghostImage)
    {

        pool.Enqueue(ghostImage);

        ghostImage.SetActive(false);

    }
    public GameObject GetGhostImageFromPool()
    {
        if(pool.Count <= 0)
        {
            FillPool();
        }
        GameObject ghostImage= pool.Dequeue();
        ghostImage.SetActive(true);
        return ghostImage;
    }

    
 
}