using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Text scoreText;
    public Text livesText;
    public GameObject arcMeteorPrefab;
    public GameObject explodingMeteorPrefab;
    public float spawnRate = 2.0f;
    public ScrollingBackground scrollingBackground;
    
    private float _currentMeteorSpeed = 1f;
    private const float SpeedIncreaseAmount = 0.5f;
    private static int _scoreToIncreaseSpeed = 100;
    private static int _scoreToIncreaseShards = 300;
    private static int _scoreToIncreaseSelfDestructTime = 200;

    private int _score = 0;
    private int _lives = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InvokeRepeating(nameof(SpawnMeteor), spawnRate, spawnRate);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ExplodingMeteor explodingMeteor = explodingMeteorPrefab.GetComponent<ExplodingMeteor>();
         if (explodingMeteor != null)
         {
             explodingMeteor.numberOfShards = 3;
         }
        if (scoreText == null)
            scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        if (livesText == null)
            livesText = GameObject.Find("LivesText").GetComponent<Text>();
        if (scrollingBackground == null)
            scrollingBackground = FindObjectOfType<ScrollingBackground>();

        scoreText.text = $"Score: {_score}";
        UpdateLivesText();
    }

    public void AddScore(int points)
    {
        _score += points;
        scoreText.text = $"Score: {_score}";
        AdjustGameDifficulty();
    }

    public void LoseLife()
    {
        _lives--;
        UpdateLivesText();

        if (_lives <= 0)
        {
            GameOver();
        }
    }

    private void AdjustGameDifficulty()
    {
        if (_score >= _scoreToIncreaseSpeed)
        {
            _currentMeteorSpeed += SpeedIncreaseAmount;
            _scoreToIncreaseSpeed += 100;
        }
        if (scrollingBackground != null)
        {
            scrollingBackground.SetScrollSpeed(_currentMeteorSpeed * 0.5f);
        }
        if (_score >= _scoreToIncreaseShards)
        {
            ExplodingMeteor explodingMeteor = explodingMeteorPrefab.GetComponent<ExplodingMeteor>();
            if (explodingMeteor != null)
            {
                explodingMeteor.numberOfShards++;
            }

            _scoreToIncreaseShards += 300;
        }
        if (_score >= _scoreToIncreaseSelfDestructTime)
        {
            ArcMeteor.TimeToSelfDestruct += 0.25f;
            _scoreToIncreaseSelfDestructTime += 200;
        }
    }

    private void UpdateLivesText()
    {
        livesText.text = $"Lives: {_lives}";
    }

    private void GameOver()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void SpawnMeteor()
    {
        float xPosition = Random.Range(-8.0f, 8.0f);
        Vector2 spawnPosition = new Vector2(xPosition, 6.0f);
        GameObject meteorPrefab = (Random.value > 0.5f) ? arcMeteorPrefab : explodingMeteorPrefab;
        Meteor meteor = meteorPrefab.GetComponent<Meteor>();
        if (meteor != null)
        {
            meteor.speed = _currentMeteorSpeed;
        }

        Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);
    }

    // public int GetLives()
    // {
    //     return _lives;
    // }
}