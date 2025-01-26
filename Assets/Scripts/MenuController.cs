using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    // Referências aos painéis
    [Header("Menu Controller")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject menuPanel;

    // Método para ir ao jogo (carregar a cena do jogo)
    public void GoToGame()
    {
        Debug.Log("Ir para o jogo");
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


}
