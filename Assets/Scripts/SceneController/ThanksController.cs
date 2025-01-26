using UnityEngine;
using UnityEngine.SceneManagement;

public class ThanksController : MonoBehaviour
{




    [SerializeField] private GameObject menuPanel;


    public void ReturnToMenu()
    {
        menuPanel.SetActive(false);
        GameController.Instance.ReturnToMenu();
        GameController.Instance.currentSceneIndex = 1;
        GameController.Instance._textPausePanel.SetActive(false);
    }
}
