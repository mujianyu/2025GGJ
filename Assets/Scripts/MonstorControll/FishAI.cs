using UnityEngine;

public class FishAI : MonoBehaviour
{
    public float speed = 2f;
    public float detectionRadius = 2f; // 主角泡泡的检测范围
    public GameObject bubble; // 主角的泡泡
    private Rigidbody2D rb2d;
    private Vector2 cameraPosition;
    private bool isActive = true; // 用于检测当前鱼群是否活跃

    private float timeInCamera = 0f;
    private float maxTimeInCamera = 10f; // 鱼群在摄像机内活跃的时间

    private FishManager manager; // 鱼群管理器引用

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        cameraPosition = Camera.main.transform.position;
        manager = FindObjectOfType<FishManager>(); // 获取 FishManager
    }

    private void Update()
    {
        if (!isActive) { 

        }
            //return;  // 如果不是活跃状态则跳过更新

        // 如果鱼群在摄像机内，计时
        if (Vector2.Distance(transform.position, cameraPosition) < 300f)  // 如果鱼群在摄像机视野内
        {
            timeInCamera += Time.deltaTime;
            if (timeInCamera >= maxTimeInCamera)  // 超过最大时间后禁用
            {
                MoveOutOfCamera();
            }
            else
            {
                RandomMove();
            }
        }
        else
        {
            // 鱼群游出摄像机外，禁用
            MoveOutOfCamera();
        }
    }

    private void MoveOutOfCamera()
    {
        Vector2 pos = transform.position;
        Vector2 moveDirection = (pos - cameraPosition).normalized;
        rb2d.velocity = moveDirection * speed;
        if (Vector2.Distance(transform.position, cameraPosition) > 100f)
        {
            DeactivateFish();
        }
    }
    private void RandomMove()
    {
        Vector2 randomDirection = new Vector2(100f,0f).normalized;
        rb2d.velocity = randomDirection * speed;

        // 如果鱼群碰到泡泡，戳破泡泡
        if (Vector2.Distance(transform.position, bubble.transform.position) < detectionRadius)
        {
            if (Random.value < 0.33f)
            {
                Destroy(bubble); // 33% 概率戳破泡泡
            }
        }
    }

    // 激活鱼群
    public void ActivateFish(Vector2 spawnPosition)
    {
        isActive = true;
        timeInCamera = 0f; // 重置计时
        transform.position = spawnPosition; // 设置初始位置
        gameObject.SetActive(true);  // 激活对象
    }

    // 禁用鱼群并返回对象池
    public void DeactivateFish()
    {
        isActive = false;
        gameObject.SetActive(false);  // 禁用对象
        manager.ReturnFishToPool(this);  // 将对象归还到池中
    }
}
