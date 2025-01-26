using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    // Referências aos painéis
    [Header("Menu Controller")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private ParticleSystem menuParticles;
    [SerializeField] private ParticleSystem menuParticles1;

    // Referência ao sistema de partículas


    private void Start()
    {
        OnEnterMenu();
    }


    // Método para ir ao jogo (carregar a cena do jogo)
    public void GoToGame()
    {
        OnExitMenu();
        Debug.Log("Ir para o jogo");
        GameController.Instance._textPausePanel.SetActive(true);
        SceneManager.LoadScene("Game 1");
    }

    // Método para abrir o painel de configurações
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    // Método para abrir o painel de créditos
    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    // Método para fechar todos os painéis (botão voltar)
    public void CloseAllPanels()
    {
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    // Método para sair do jogo
    public void QuitGame()
    {
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }

    public void OnEnterMenu()
    {
        // Verifica se o sistema de partículas está definido e inicia as partículas
        if (menuParticles != null)
        {
            menuParticles.Play();
            menuParticles1.Play();
        }
        else
        {
            Debug.LogWarning("MenuParticles não está atribuído no inspector!");
        }
    }

    public void OnExitMenu()
    {
        // Para as partículas quando sair do menu
        if (menuParticles != null)
        {
            menuParticles.Stop();
            menuParticles1.Stop();
        }
    }


}
