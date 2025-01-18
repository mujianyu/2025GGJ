using System.Collections.Generic;
using UnityEngine;

public class SharkManager : MonoBehaviour
{
    public GameObject sharkPrefab;  // ����Ԥ����
    public GameObject player;  // ���Ƕ���
    public int poolSize = 2;  // �������������
    private List<SharkAI> sharkPool;
    private float timeSinceLastSpawn = 0f;
    private float spawnInterval = 10f;

    private void Start()
    {
        sharkPool = new List<SharkAI>();
        // ��ʼ�������
        for (int i = 0; i < poolSize; i++)
        {
            GameObject shark = Instantiate(sharkPrefab);
            SharkAI sharkAI = shark.GetComponent<SharkAI>();
            sharkAI.player = player;
            shark.SetActive(false); // һ��ʼ������
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

    // ����һ������
    private void SpawnShark()
    {
        SharkAI sharkAI = GetInactiveShark();
        if (sharkAI != null)
        {
            Vector2 spawnPosition = new Vector2(Camera.main.transform.position.x + Random.Range(-10f, -5f), Camera.main.transform.position.y + Random.Range(-5f, 5f));
            sharkAI.ActivateShark(spawnPosition); // ��������
        }
    }

    // ��ȡһ��δ���������
    private SharkAI GetInactiveShark()
    {
        foreach (var shark in sharkPool)
        {
            if (!shark.gameObject.activeInHierarchy)
            {
                return shark;  // ����һ��δ���������
            }
        }
        return null;  // �������û�п��õ����㣬���� null
    }

    // ��������󷵻ص�����
    public void ReturnSharkToPool(SharkAI shark)
    {
        // ���ض���أ����´�ʹ��
    }
}
