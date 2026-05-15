using UnityEngine;

public class Pufferfish : CharacterBase
{
    public float moveSpeed = 2f;
    public float verticalSpeed = 2f;

    private float topY;
    private float bottomY;
    private float targetY;

    private bool movingUp = true;

    private void Awake()
    {
        gameObject.tag = "Obstacle";
    }

    private void Start()
    {
        GeneratePoints();
        targetY = topY;
    }

    private void Update()
    {
        // Move left constantly
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        // Move vertically toward target
        Vector3 pos = transform.position;
        pos.y = Mathf.MoveTowards(pos.y, targetY, verticalSpeed * Time.deltaTime);
        transform.position = pos;

        // Switch direction when reaching target
        if (Mathf.Abs(transform.position.y - targetY) < 0.05f)
        {
            movingUp = !movingUp;
            targetY = movingUp ? topY : bottomY;
        }
    }

    private void GeneratePoints()
    {
        float minY = -2.75f;
        float maxY = 4.6f;

        // ensure at least 4 units apart
        do
        {
            bottomY = Random.Range(minY, maxY - 5f);
            topY = Random.Range(bottomY + 5f, maxY);

        } while (topY - bottomY < 5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}