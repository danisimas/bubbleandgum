using UnityEngine;

public class ActionsForFixedTimeScale : MonoBehaviour
{
    [SerializeField] private GameObject _bubbleObject;
    [SerializeField] private GameObject _gumObject;

    private float _previousTimeScale = 1f; // Armazena o último valor do Time.timeScale

    private void Start()
    {
        // Salva o valor inicial de Time.timeScale
        _previousTimeScale = Time.timeScale;
        UpdateCharacterActionScripts();
    }

    private void Update()
    {
        // Verifica se o Time.timeScale mudou
        if (!Mathf.Approximately(_previousTimeScale, Time.timeScale))
        {
            _previousTimeScale = Time.timeScale;
            UpdateCharacterActionScripts();
        }
    }

    private void UpdateCharacterActionScripts()
    {
        // Determina se os scripts devem estar ativados ou desativados com base no Time.timeScale
        bool scriptIsEnabled = Time.timeScale > 0;
        CharacterActionScriptsIsEnabled(scriptIsEnabled);
    }

    private void CharacterActionScriptsIsEnabled(bool scriptIsEnabled)
    {
        // Ativa ou desativa os scripts de ação dos personagens
        _gumObject.GetComponent<SplitInHalf>().enabled = scriptIsEnabled;
        _bubbleObject.GetComponent<DoubleJump>().enabled = scriptIsEnabled;
        _gumObject.GetComponent<PlayerController>().enabled = scriptIsEnabled;
        _bubbleObject.GetComponent<PlayerController>().enabled = scriptIsEnabled;
    }
}
