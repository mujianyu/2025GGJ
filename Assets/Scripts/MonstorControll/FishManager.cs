using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    public GameObject fishPrefab;  // ��ȺԤ����
    public GameObject bubble;  // ���ǵ�����
    public int poolSize = 10; // ������Ⱥ������
    private List<FishAI> fishPool; // �洢����ص��б�
    private float timeSinceLastSpawn = 0f;
    private float spawnInterval = 5f; // ÿ��10��������Ⱥ

    private void Start()
    {
        fishPool = new List<FishAI>();

        // ��ʼ������أ����������Ⱥ����������
        for (int i = 0; i < poolSize; i++)
        {
            GameObject fish = Instantiate(fishPrefab);
            FishAI fishAI = fish.GetComponent<FishAI>();
            fishAI.bubble = bubble;
            fish.SetActive(false); // ��Ⱥ��ʼ״̬Ϊ����
            fishPool.Add(fishAI); // ����Ⱥ�������
        }
    }

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnFish();
            timeSinceLastSpawn = 0f; // ���ü�ʱ��
        }
    }

    // ����һ���µ���Ⱥ
    private void SpawnFish()
    {
        FishAI fishAI = GetInactiveFish();  // ��ȡ���е�һ��δ�������Ⱥ
        if (fishAI != null)
        {
            Vector2 spawnPosition = new Vector2(Camera.main.transform.position.x + Random.Range(-10f, -5f), Camera.main.transform.position.y + Random.Range(-5f, 5f));
            fishAI.ActivateFish(spawnPosition);  // ������Ⱥ������λ��
        }
    }

    // �ӳ��л�ȡһ��δ�������Ⱥ
    private FishAI GetInactiveFish()
    {
        foreach (var fish in fishPool)
        {
            if (!fish.gameObject.activeInHierarchy)
            {
                return fish;  // ����һ��δ�������Ⱥ
            }
        }
        return null;  // �������û�п��õ���Ⱥ������ null
    }

    // ����Ⱥ����黹������
    public void ReturnFishToPool(FishAI fish)
    {
        // ����Ⱥ���ú����� DeactivateFish �����˹黹����
        // ������������һЩ�����������ָ�״̬�ȣ�
    }
}
