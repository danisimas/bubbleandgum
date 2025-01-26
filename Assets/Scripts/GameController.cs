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
    [HideInInspector] public bool isVictory = false;
    [HideInInspector] public string currentSceneName;

    private int playerLives = 1;
    public int counterVictory = 0;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(transform.parent.gameObject);
            currentSceneName = SceneManager.GetActiveScene().name;
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
        isVictory = false;
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

        if (isVictory && counterVictory >= 4)
        {
            Victory();
        }
        else if (counterVictory < 0)
        {
            counterVictory = 0;
        }


    }




    public void RestartGame()
    {
        ResetGame();
        Debug.Log("Jogo Reiniciado!");
        SceneManager.LoadScene(currentSceneName);
    }



    private void ResetGame()
    {
        isGameOver = false;
        isVictory = false;
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

        if (_textPausePanel != null)
            _textPausePanel.SetActive(false);



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
        if (_textPausePanel != null)
            _textPausePanel.SetActive(true);
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

        if (_textPausePanel != null)
            _textPausePanel.SetActive(false);
    }

    public void ReturnToMenu()
    {
        currentSceneName = "Menu";
        ResetGame();
        SceneManager.LoadScene("Menu");
    }


    public void LoadNextScene()
    {
        // Obtemos o índice da cena atual
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Calcula o índice da próxima cena
        int nextSceneIndex = currentSceneIndex + 1;

        // Verifica se há mais cenas no Build Settings
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            ResetGame();
            SceneManager.LoadScene(nextSceneIndex); // Carrega a próxima cena
        }
        else
        {
            ResetGame();
            Debug.Log("Você chegou na última cena! Reiniciando para a primeira...");
            SceneManager.LoadScene(0); // Reinicia a primeira cena
        }
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
            if (_textPausePanel != null)
                _textPausePanel.SetActive(false);
        }
    }
}
