using UnityEngine;

public class Worm : CharacterBase
{
    [SerializeField] private int bonusAmount = 5;

    private void Update()
    {
        transform.position += 2 * Vector3.left * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.curScore += bonusAmount;
        Destroy(gameObject);
    }

}
