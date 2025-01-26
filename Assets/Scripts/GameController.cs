using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    public static GameController Instance; // Singleton para acesso global

    [Header("Game Controller")]
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _derrotaPanel;
    [SerializeField] private GameObject _vitoriaPanel;
    [SerializeField] private GameObject _proximoPanel;
    [SerializeField] private GameObject _textPausePanel;


    [HideInInspector] public bool _gameIsPaused = false;
    [HideInInspector] public bool isGameOver = false;
    private int playerLives = 1;
    public int counterVictory = 0;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(transform.parent.gameObject);
        }
        else
        {
            Destroy(transform.parent.gameObject);
        }
    }

    private void Start()
    {
        _gameIsPaused = false;
        isGameOver = false;
        counterVictory = 0;
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
        counterVictory = 0;

        if (_derrotaPanel != null)
            _derrotaPanel.SetActive(false);

        if (_pausePanel != null)
            _pausePanel.SetActive(false);

        if (_vitoriaPanel != null)
            _vitoriaPanel.SetActive(false);

        if (_proximoPanel != null)
            _proximoPanel.SetActive(false);

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

    public void Victory()
    {
        if (counterVictory >= 4)
        {
            Debug.Log("Victory!");

            isGameOver = true;
            StopTime();

            if (_vitoriaPanel != null)
                _vitoriaPanel.SetActive(true);
        }
    }
}
