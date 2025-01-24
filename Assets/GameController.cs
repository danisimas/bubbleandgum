using UnityEngine;

public class GameController : MonoBehaviour
{

    [Header("Game Controller")]

    [SerializeField] private GameObject _pausePanel;


    [SerializeField] private GameObject[] enemyPrefabs;

    private bool _gameIsPaused;




    private void Awake()
    {
        // Configurar Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    private void Update()
    {
        if (isGameOver)
        {
            RestartGame();
        }

        if (Input.GetKeyDown(KeyCode.ESCAPE))
        {
            if (!_gameIsPaused)
                EventManager.OnGamePauseTrigger();
            else if (_gameIsPaused)
                EventManager.OnResumeGameTrigger();
        }
    }


    public void RestartGame()
    {
        isGameOver = false;
        Debug.Log("Jogo Reiniciado!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void PauseGame()
    {
        StopTime();
        _pausePanel.SetActive(true);
        _settingsPanel.SetActive(false);
        _gameIsPaused = true;
    }

    private void UnpauseGame()
    {
        _pausePanel.SetActive(false);
        _settingsPanel.SetActive(false);
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
}

