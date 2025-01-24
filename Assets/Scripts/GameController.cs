using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance; // Singleton para acesso global

    [Header("Game Controller")]
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _derrotaPanel;
    [SerializeField] private GameObject _bubbleObject;
    [SerializeField] private GameObject _gumObject;

    private bool _gameIsPaused;
    private int characterControlMode = 0;
    private int playerLives = 1;
    private bool isGameOver = false;

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

        ChangeCharacterControl();
    }

    private void ChangeCharacterControl()
    {
        if ((Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)))
        {
            if (characterControlMode == 2) characterControlMode = 0;
            else characterControlMode++;

            switch (characterControlMode)
            {
                case 1: // Controle apenas do Gum
                    ChangeActivedInBubble();
                    break;
                case 2: // Controle apenas da Bubble
                    ChangeActivedInBubble();
                    ChangeActivedInGum();
                    break;
                default: // Controle de ambos os personagens
                    ChangeActivedInGum();
                    break;
            }

        }

    }

    private void ChangeActivedInBubble()
    {


        if (characterControlMode == 1)
        {
            _bubbleObject.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;

            // coloca animação em idle
            _bubbleObject.GetComponentInChildren<Animator>().SetBool("Walk", false);
            _bubbleObject.GetComponentInChildren<Animator>().SetBool("Jump", false);
            _bubbleObject.GetComponentInChildren<Animator>().SetBool("idle", true);

            // desativa os movimentos
            _bubbleObject.GetComponent<PlayerController>().enabled = false;
            _bubbleObject.GetComponent<DoubleJump>().enabled = false;
        }
        else
        {
            _bubbleObject.GetComponent<PlayerController>().enabled = true; // Ativa o movimento
            _bubbleObject.GetComponent<DoubleJump>().enabled = true; // Ativa o spawn de plataforma com pulo duplo
        }

    }

    private void ChangeActivedInGum()
    {
        if (characterControlMode == 2)
        {
            _gumObject.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;

            // coloca a animação em idle
            _gumObject.GetComponentInChildren<Animator>().SetBool("Walk", false);
            _gumObject.GetComponentInChildren<Animator>().SetBool("Jump", false);
            _gumObject.GetComponentInChildren<Animator>().SetBool("idle", true);

            // desativa os movimentos
            _gumObject.GetComponent<PlayerController>().enabled = false;
            _gumObject.GetComponent<SplitInHalf>().enabled = false;
        }
        else
        {
            _gumObject.GetComponent<PlayerController>().enabled = true; // Ativa o movimento
            _gumObject.GetComponent<SplitInHalf>().enabled = true; // Ativa a divisão com spawn de plataforma
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

    public void ExitGame()
    {
        Debug.Log("O jogo foi encerrado!"); // Apenas para teste no Editor
        Application.Quit();
    }
}
