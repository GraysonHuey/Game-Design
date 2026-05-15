using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player player;
    [SerializeField] private Spawner spawner;
    [SerializeField] public Text scoreText;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject shopButton;
    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject gameOver;

    [SerializeField] private GameObject shieldIcon;
    [SerializeField] private GameObject playingShieldIcon;
    [SerializeField] private int shieldPrice;
    [SerializeField] private GameObject shieldPriceText;
    [SerializeField] private GameObject shieldPurchase;
    [SerializeField] private GameObject shieldsOwnedText;
    [SerializeField] public GameObject playingShieldsOwnedText;

    [SerializeField] private GameObject shotIcon;
    [SerializeField] private GameObject playingShotIcon;
    [SerializeField] private int shotPrice;
    [SerializeField] private GameObject shotPriceText;
    [SerializeField] private GameObject shotPurchase;
    [SerializeField] private GameObject shotsOwnedText;
    [SerializeField] public GameObject playingShotsOwnedText;

    public static int curScore = 0;
    public static int totalScore = 0;
    public static Dictionary<string, int> items = new Dictionary<string, int>()
    {
        {"Shield", 0},
        {"Shot", 0}
    };

    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
        }

        playButton.SetActive(true);
        shopButton.SetActive(true);
        backButton.SetActive(false);
        gameOver.SetActive(true);

        shieldIcon.SetActive(false);
        playingShieldIcon.SetActive(false);
        shieldPriceText.SetActive(false);
        shieldPurchase.SetActive(false);
        shieldsOwnedText.SetActive(false);
        playingShieldsOwnedText.SetActive(false);

        shotIcon.SetActive(false);
        playingShotIcon.SetActive(false);
        shotPriceText.SetActive(false);
        shotPurchase.SetActive(false);
        shotsOwnedText.SetActive(false);
        playingShotsOwnedText.SetActive(false);
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start()
    {
        Pause();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void Play()
    {
        curScore = 0;
        scoreText.text = curScore.ToString();

        playButton.SetActive(false);
        shopButton.SetActive(false);
        backButton.SetActive(false);
        shieldIcon.SetActive(false);
        playingShieldIcon.SetActive(true);
        shieldPriceText.SetActive(false);
        shieldPurchase.SetActive(false);
        shieldsOwnedText.SetActive(false);
        playingShieldsOwnedText.SetActive(true);
        shotIcon.SetActive(false);
        playingShotIcon.SetActive(true);
        shotPriceText.SetActive(false);
        shotPurchase.SetActive(false);
        shotsOwnedText.SetActive(false);
        playingShotsOwnedText.SetActive(true);
        gameOver.SetActive(false);

        Time.timeScale = 1f;
        player.enabled = true;

        ScoreZone[] scorezones = FindObjectsOfType<ScoreZone>();

        for (int i = 0; i < scorezones.Length; i++) {
            Destroy(scorezones[i].gameObject);
        }
    }

    public void GameOver()
    {
        scoreText.text = curScore.ToString();
        Debug.Log("Total score = " + totalScore);

        CharacterBase[] characters = FindObjectsOfType<CharacterBase>();
        for (int i = 0; i < characters.Length; i++) {
            Destroy(characters[i].gameObject);
        }

        ScoreZone[] scorezones = FindObjectsOfType<ScoreZone>();
        for (int i = 0; i < scorezones.Length; i++) {
            Destroy(scorezones[i].gameObject);
        }

        player.ResetState();

        playButton.SetActive(true);
        shopButton.SetActive(true);
        backButton.SetActive(false);
        gameOver.SetActive(true);

        shieldIcon.SetActive(false);
        playingShieldIcon.SetActive(false);
        shieldPriceText.SetActive(false);
        shieldPurchase.SetActive(false);
        shieldsOwnedText.SetActive(false);
        playingShieldsOwnedText.SetActive(false);

        shotIcon.SetActive(false);
        playingShotIcon.SetActive(false);
        shotPriceText.SetActive(false);
        shotPurchase.SetActive(false);
        shotsOwnedText.SetActive(false);
        playingShotsOwnedText.SetActive(false);

        Pause();
    }

    public void Shop()
    {
        playButton.SetActive(false);
        shopButton.SetActive(false);
        backButton.SetActive(true);
        gameOver.SetActive(false);

        scoreText.text = "Total score: " + totalScore;

        shieldPriceText.GetComponent<Text>().text = "Cost: " + shieldPrice;
        shieldsOwnedText.GetComponent<Text>().text = "Have: " + items["Shield"];

        shotPriceText.GetComponent<Text>().text = "Cost: " + shotPrice;
        shotsOwnedText.GetComponent<Text>().text = "Have: " + items["Shot"];

        shieldIcon.SetActive(true);
        shieldPriceText.SetActive(true);
        shieldPurchase.SetActive(true);
        shieldsOwnedText.SetActive(true);

        shotIcon.SetActive(true);
        shotPriceText.SetActive(true);
        shotPurchase.SetActive(true);
        shotsOwnedText.SetActive(true);
    }

    public void Purchase(string item) 
    {
        if (item == "Shield")
        {
            if (totalScore >= shieldPrice) 
            {
                totalScore -= shieldPrice;
                items["Shield"] += 1;
            }
        }
        else if (item == "Shot") 
        {
            if (totalScore >= shotPrice)
            {
                totalScore -= shotPrice;
                items["Shot"] += 1;
            }
        }
        shieldsOwnedText.GetComponent<Text>().text = "Have: " + items["Shield"];
        shotsOwnedText.GetComponent<Text>().text = "Have: " + items["Shot"];
        scoreText.text = "Total score: " + totalScore;
    }

    public void IncreaseScore()
    {
        curScore++;
        scoreText.text = curScore.ToString();
    }

}
