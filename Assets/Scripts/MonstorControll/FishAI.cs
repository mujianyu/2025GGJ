using UnityEngine;

public class FishAI : MonoBehaviour
{
    public float speed = 2f;
    public float detectionRadius = 2f; // �������ݵļ�ⷶΧ
    public GameObject bubble; // ���ǵ�����
    private Rigidbody2D rb2d;
    private Vector2 cameraPosition;
    private bool isActive = true; // ���ڼ�⵱ǰ��Ⱥ�Ƿ��Ծ

    private float timeInCamera = 0f;
    private float maxTimeInCamera = 10f; // ��Ⱥ��������ڻ�Ծ��ʱ��

    private FishManager manager; // ��Ⱥ����������

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        cameraPosition = Camera.main.transform.position;
        manager = FindObjectOfType<FishManager>(); // ��ȡ FishManager
    }

    private void Update()
    {
        if (!isActive) { 

        }
            //return;  // ������ǻ�Ծ״̬����������

        // �����Ⱥ��������ڣ���ʱ
        if (Vector2.Distance(transform.position, cameraPosition) < 300f)  // �����Ⱥ���������Ұ��
        {
            timeInCamera += Time.deltaTime;
            if (timeInCamera >= maxTimeInCamera)  // �������ʱ������
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
            // ��Ⱥ�γ�������⣬����
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

        // �����Ⱥ�������ݣ���������
        if (Vector2.Distance(transform.position, bubble.transform.position) < detectionRadius)
        {
            if (Random.value < 0.33f)
            {
                Destroy(bubble); // 33% ���ʴ�������
            }
        }
    }

    // ������Ⱥ
    public void ActivateFish(Vector2 spawnPosition)
    {
        isActive = true;
        timeInCamera = 0f; // ���ü�ʱ
        transform.position = spawnPosition; // ���ó�ʼλ��
        gameObject.SetActive(true);  // �������
    }

    // ������Ⱥ�����ض����
    public void DeactivateFish()
    {
        isActive = false;
        gameObject.SetActive(false);  // ���ö���
        manager.ReturnFishToPool(this);  // ������黹������
    }
}
