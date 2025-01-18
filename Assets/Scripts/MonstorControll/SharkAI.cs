using UnityEngine;

public class SharkAI : MonoBehaviour
{
    public float speed = 3f;
    public GameObject player;  // 主角对象
    private Rigidbody2D rb2d;
    private Vector2 cameraPosition;
    private bool isActive = true;

    private float chaseTime = 0f;
    private SharkManager manager; // 鲨鱼管理器引用

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        cameraPosition = Camera.main.transform.position;
        manager = FindObjectOfType<SharkManager>(); // 获取 SharkManager
    }

    private void Update()
    {
        if (!isActive) return;  // 如果鲨鱼不活跃则跳过更新

        if (chaseTime < 13f)
        {
            ChasePlayer();
            chaseTime += Time.deltaTime;
        }
        else
        {
            MoveOutOfCamera();
        }
    }

    private void ChasePlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb2d.velocity = direction * speed;
    }

    private void MoveOutOfCamera()
    {
        Vector2 pos = transform.position;
        Vector2 moveDirection = ( pos - cameraPosition).normalized;
        rb2d.velocity = moveDirection * speed;
        if (Vector2.Distance(transform.position, cameraPosition) > 100f)
        {
            DeactivateShark(); // 禁用鲨鱼
        }
       
    }

    // 激活鲨鱼
    public void ActivateShark(Vector2 spawnPosition)
    {
        isActive = true;
        chaseTime = 0f;
        transform.position = spawnPosition;
        gameObject.SetActive(true);  // 激活对象
    }

    // 禁用鲨鱼并返回对象池
    public void DeactivateShark()
    {
        isActive = false;
        gameObject.SetActive(false);  // 禁用对象
        manager.ReturnSharkToPool(this);  // 将对象归还到池中
    }
}
