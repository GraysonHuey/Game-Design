using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float speed = 8f;
    public float lifeTime = 30f;

    private float timer;

    private void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;

        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
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