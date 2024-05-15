using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // References to other GameObjects and UI elements
    public GameObject pillarPrefab1;
    public GameObject pillarPrefab2;
    public Transform bird;
    public Transform sky;
    public Transform ground;
    public GameObject end;

    // HighScoreManager script reference
    public HighScoreManager highScoreManager;

    // Pillar spawn settings
    public float pillarSpawnInterval = 2f;
    public float pillarSpawnRange = 2f;
    public float minIntervalSpeed = 1f;
    public float maxIntervalSpeed = 2f;
    public float intervalSpeedIncrease = 0.1f;

    // UI elements for displaying scores
    public Text scoreText;
    public Text scoreText2;

    // Timer and score variables
    private float timer;
    private float currentIntervalSpeed;
    private int score = 0;

    void Start()
    {
        currentIntervalSpeed = pillarSpawnInterval;
        end.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= currentIntervalSpeed)
        {
            SpawnPillar();
            timer = 0f;
            UpdateIntervalSpeed();
        }
    }

    void SpawnPillar()
    {
        Vector3 spawnPosition = new Vector3(bird.position.x + pillarSpawnRange, 0f, 0f);
        GameObject newPillar;
        float randomHeight = Random.Range(1f, 2f);

        if (Random.Range(0, 2) == 0)
        {
            float randomY = Random.Range(sky.position.y, sky.position.y + 1f);
            spawnPosition.y = randomY;
            newPillar = Instantiate(pillarPrefab1, spawnPosition, Quaternion.identity);
            newPillar.transform.localScale = new Vector3(1f, randomHeight, 1f);
            newPillar.transform.Rotate(Vector3.forward, 180f);
        }
        else
        {
            float randomY = Random.Range(ground.position.y - 1f, ground.position.y);
            spawnPosition.y = randomY;
            newPillar = Instantiate(pillarPrefab2, spawnPosition, Quaternion.identity);
            newPillar.transform.localScale = new Vector3(1f, randomHeight, 1f);
        }

        Destroy(newPillar, 20f);
    }

    void UpdateIntervalSpeed()
    {
        currentIntervalSpeed = Mathf.Clamp(currentIntervalSpeed - intervalSpeedIncrease, minIntervalSpeed, maxIntervalSpeed);
    }

    public void IncreaseScore()
    {
        score++;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score : " + score.ToString();
            scoreText2.text = "Your Score : " + score.ToString();
        }
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }

    public void CloseApplication()
    {
        Application.Quit();
    }

    public void EndGame()
    {
        Debug.Log("Game ended");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        end.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;

        // Set the current score in the HighScoreManager script
        highScoreManager.SetCurrentScore(score);
    }

    // Function to be called when the button beside the input field is clicked
    public void SaveScoreAndName()
    {
        // Call the HighScoreManager's function to save the score and name
        highScoreManager.SaveHighScore();

        // Optionally, update the high score display
        highScoreManager.DisplayTopHighScores();
    }
}
