using UnityEngine;

public class RustyCan : CharacterBase
{
    private void Awake() 
    {
        gameObject.tag = "Obstacle";
    }

    private void Update()
    {
        transform.position += 2 * Vector3.left * Time.deltaTime;
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