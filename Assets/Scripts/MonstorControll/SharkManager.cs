using System.Collections.Generic;
using UnityEngine;

public class SharkManager : MonoBehaviour
{
    public GameObject sharkPrefab;  // 鲨鱼预设体
    public GameObject player;  // 主角对象
    public int poolSize = 2;  // 池中鲨鱼的数量
    private List<SharkAI> sharkPool;
    private float timeSinceLastSpawn = 0f;
    private float spawnInterval = 10f;

    private void Start()
    {
        sharkPool = new List<SharkAI>();
        // 初始化对象池
        for (int i = 0; i < poolSize; i++)
        {
            GameObject shark = Instantiate(sharkPrefab);
            SharkAI sharkAI = shark.GetComponent<SharkAI>();
            sharkAI.player = player;
            shark.SetActive(false); // 一开始不激活
            sharkPool.Add(sharkAI);
        }
    }

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnShark();
            timeSinceLastSpawn = 0f;
        }
    }

    // 生成一个鲨鱼
    private void SpawnShark()
    {
        SharkAI sharkAI = GetInactiveShark();
        if (sharkAI != null)
        {
            Vector2 spawnPosition = new Vector2(Camera.main.transform.position.x + Random.Range(-10f, -5f), Camera.main.transform.position.y + Random.Range(-5f, 5f));
            sharkAI.ActivateShark(spawnPosition); // 激活鲨鱼
        }
    }

    // 获取一个未激活的鲨鱼
    private SharkAI GetInactiveShark()
    {
        foreach (var shark in sharkPool)
        {
            if (!shark.gameObject.activeInHierarchy)
            {
                return shark;  // 返回一个未激活的鲨鱼
            }
        }
        return null;  // 如果池中没有可用的鲨鱼，返回 null
    }

    // 将鲨鱼对象返回到池中
    public void ReturnSharkToPool(SharkAI shark)
    {
        // 返回对象池，供下次使用
    }
}
