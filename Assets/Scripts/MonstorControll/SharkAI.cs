using UnityEngine;

public class SharkAI : MonoBehaviour
{
    public float speed = 3f;
    public GameObject player;  // ���Ƕ���
    private Rigidbody2D rb2d;
    private Vector2 cameraPosition;
    private bool isActive = true;

    private float chaseTime = 0f;
    private SharkManager manager; // �������������

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        cameraPosition = Camera.main.transform.position;
        manager = FindObjectOfType<SharkManager>(); // ��ȡ SharkManager
    }

    private void Update()
    {
        if (!isActive) return;  // ������㲻��Ծ����������

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
            DeactivateShark(); // ��������
        }
       
    }

    // ��������
    public void ActivateShark(Vector2 spawnPosition)
    {
        isActive = true;
        chaseTime = 0f;
        transform.position = spawnPosition;
        gameObject.SetActive(true);  // �������
    }

    // �������㲢���ض����
    public void DeactivateShark()
    {
        isActive = false;
        gameObject.SetActive(false);  // ���ö���
        manager.ReturnSharkToPool(this);  // ������黹������
    }
}
