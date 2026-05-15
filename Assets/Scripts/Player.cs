using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float strength = 5f;
    public float gravity = -9.81f;
    public float tilt = 5f;

    private SpriteRenderer spriteRenderer;
    private Vector3 direction;
    private Vector3 startPosition;
    private int spriteIndex;
    private bool shieldActive;
    private GameObject shield;

    public GameObject bubblePrefab;
    public Transform bubbleSpawnPoint;
    public float shootCooldown = 0.3f;
    public AudioSource audioSource;
    public AudioClip shootSound;
    public AudioClip flopSound;

    private float shootTimer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.position;
    }

    private void OnEnable()
    {
        ResetState();
    }

    public void ResetState()
    {
        transform.position = startPosition;
        direction = Vector3.zero;
        shieldActive = false;
        shield = transform.Find("Shield").gameObject;
        shield.SetActive(false);
        shootTimer = 0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            direction = Vector3.up * strength;
            if (flopSound != null && audioSource != null)
            {
                audioSource.pitch = Random.Range(0.85f, 1.15f);
                audioSource.PlayOneShot(flopSound);
            }
        }

        if (Input.GetKeyDown(KeyCode.S) && GameManager.items["Shield"] != 0 && !shieldActive)
        {
            shieldActive = true;
            shield.SetActive(true);
            GameManager.items["Shield"] -= 1;
        }

        shootTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.F) && shootTimer <= 0f && GameManager.items["Shot"] != 0)
        {
            GameManager.items["Shot"] -= 1;
            ShootBubble();
            shootTimer = shootCooldown;
        }

        GameManager.Instance.playingShieldsOwnedText.GetComponent<Text>().text = GameManager.items["Shield"].ToString();
        GameManager.Instance.playingShotsOwnedText.GetComponent<Text>().text = GameManager.items["Shot"].ToString();

        // Apply gravity and update the position
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

        // Tilt the bird based on the direction
        Vector3 rotation = transform.eulerAngles;
        rotation.z = direction.y * tilt;
        transform.eulerAngles = rotation;
    }

    private void ShootBubble()
    {
        if (shootSound != null && audioSource != null)
        {
            audioSource.pitch = Random.Range(0.85f, 1.15f);
            audioSource.PlayOneShot(shootSound);
        }

        Vector3 spawnPos = bubbleSpawnPoint != null 
            ? bubbleSpawnPoint.position 
            : transform.position;

        Instantiate(bubblePrefab, spawnPos, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle")) {
            if (shieldActive)
            {
                shieldActive = false;
                shield.SetActive(false);
                return;
            }
            GameManager.totalScore += GameManager.curScore;
            GameManager.Instance.GameOver();
        } else if (other.gameObject.CompareTag("Scoring")) {
            GameManager.Instance.IncreaseScore();
        }
    }

}
