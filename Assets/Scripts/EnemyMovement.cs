using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 2f;
    public float width = 3f;

    private Vector3 startPosition;
    private float randomOffset;
    private float lastX;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        startPosition = transform.localPosition;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        randomOffset = Random.Range(0f, 100f);
        lastX = transform.localPosition.x;
    }

    void Update()
    {
        float movement = Mathf.PingPong((Time.time + randomOffset) * speed, width * 2);
        float xOffset = movement - width;
        float currentX = startPosition.x + xOffset;
       
        if (currentX > lastX)
        {        
            spriteRenderer.flipX = false;
        }
        else if (currentX < lastX)
        {  
            spriteRenderer.flipX = true;
        }
        lastX = currentX;
        transform.localPosition = new Vector3(currentX, startPosition.y, startPosition.z);
    }
}