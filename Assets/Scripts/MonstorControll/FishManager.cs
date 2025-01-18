using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    public GameObject fishPrefab;  // 鱼群预设体
    public GameObject bubble;  // 主角的泡泡
    public int poolSize = 10; // 池中鱼群的数量
    private List<FishAI> fishPool; // 存储对象池的列表
    private float timeSinceLastSpawn = 0f;
    private float spawnInterval = 5f; // 每隔10秒生成鱼群

    private void Start()
    {
        fishPool = new List<FishAI>();

        // 初始化对象池，创建多个鱼群并禁用它们
        for (int i = 0; i < poolSize; i++)
        {
            GameObject fish = Instantiate(fishPrefab);
            FishAI fishAI = fish.GetComponent<FishAI>();
            fishAI.bubble = bubble;
            fish.SetActive(false); // 鱼群初始状态为禁用
            fishPool.Add(fishAI); // 将鱼群加入池中
        }
    }

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnFish();
            timeSinceLastSpawn = 0f; // 重置计时器
        }
    }

    // 生成一个新的鱼群
    private void SpawnFish()
    {
        FishAI fishAI = GetInactiveFish();  // 获取池中的一个未激活的鱼群
        if (fishAI != null)
        {
            Vector2 spawnPosition = new Vector2(Camera.main.transform.position.x + Random.Range(-10f, -5f), Camera.main.transform.position.y + Random.Range(-5f, 5f));
            fishAI.ActivateFish(spawnPosition);  // 激活鱼群并设置位置
        }
    }

    // 从池中获取一个未激活的鱼群
    private FishAI GetInactiveFish()
    {
        foreach (var fish in fishPool)
        {
            if (!fish.gameObject.activeInHierarchy)
            {
                return fish;  // 返回一个未激活的鱼群
            }
        }
        return null;  // 如果池中没有可用的鱼群，返回 null
    }

    // 将鱼群对象归还到池中
    public void ReturnFishToPool(FishAI fish)
    {
        // 在鱼群禁用后，已在 DeactivateFish 中做了归还操作
        // 可以在这里做一些清理操作（如恢复状态等）
    }
}
