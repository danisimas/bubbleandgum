using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance; // Singleton para acesso global

    [Header("Game Controller")]
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _derrotaPanel;

    private bool _gameIsPaused;
    private int playerLives = 1;
    private bool isGameOver = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Também torna o Canvas ou painéis persistentes
            var canvas = GameObject.Find("Canvas");
            if (canvas != null)
            {
                DontDestroyOnLoad(canvas);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        _gameIsPaused = false;
        isGameOver = false;
        Time.timeScale = 1;
    }


    private void Update()
    {
        if (!isGameOver && Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_gameIsPaused)
            {
                PauseGame();
            }
            else if (_gameIsPaused)
            {
                UnpauseGame();
            }
        }
    }

    public void RestartGame()
    {
        ResetGame();
        Debug.Log("Jogo Reiniciado!");
        Scene currentScene = SceneManager.GetActiveScene(); // Get the current scene
        SceneManager.LoadScene(currentScene.name);
    }

    private void ResetGame()
    {
        isGameOver = false;
        _gameIsPaused = false;
        playerLives = 1;

        if (_derrotaPanel != null)
            _derrotaPanel.SetActive(false);

        if (_pausePanel != null)
            _pausePanel.SetActive(false);

        RestartTime();
    }

    private void PauseGame()
    {
        StopTime();
        if (_pausePanel != null)
            _pausePanel.SetActive(true);
        _gameIsPaused = true;
    }

    private void UnpauseGame()
    {
        if (_pausePanel != null)
            _pausePanel.SetActive(false);
        RestartTime();
        _gameIsPaused = false;
    }

    private void StopTime()
    {
        Time.timeScale = 0;
    }

    private void RestartTime()
    {
        Time.timeScale = 1;
    }

    public void LoseLife(string tag)
    {
        if (isGameOver) return;

        if (tag == "Player")
        {
            playerLives--;
            Debug.Log("Vidas restantes: " + playerLives);
        }

        if (playerLives <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");

        isGameOver = true;
        StopTime();

        if (_derrotaPanel != null)
            _derrotaPanel.SetActive(true);
    }

    public void ReturnToMenu()
    {
        ResetGame();
        SceneManager.LoadScene("Menu");
    }

    public void LoadNextLevel(string sceneName)
    {
        ResetGame();
        SceneManager.LoadScene(sceneName);
    }

    public void Exit()
    {
        Debug.Log("O jogo foi encerrado!"); // Apenas para teste no Editor
        Application.Quit();
    }

    public void ExitGame()
    {
        Debug.Log("Saindo do jogo! Obrigado por jogar.");
    }
}
