using UnityEngine;
using System.Collections;

public class ControlModeChanger : MonoBehaviour
{
    private int characterControlMode = 0;

    [SerializeField] private GameObject _bubbleObject;
    [SerializeField] private GameObject _gumObject;

    [Header("Materials")]
    [SerializeField] private Material activeMaterial;   // Material para objeto ativo
    [SerializeField] private Material defaultMaterial;  // Material padrão (inativo)

    private void Update()
    {
        Debug.Log(GameController.Instance._gameIsPaused);
        Debug.Log(GameController.Instance.isGameOver);

        if (!GameController.Instance._gameIsPaused && !GameController.Instance.isGameOver)
            ChangeCharacterControl();
    }

    private void ChangeCharacterControl()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
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
                    StartCoroutine(ActivateBlinkingEffect(_bubbleObject));
                    StartCoroutine(ActivateBlinkingEffect(_gumObject));

                    break;
            }
        }
    }

    private void ChangeActivedInBubble()
    {
        if (characterControlMode == 1)
        {
            _bubbleObject.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;

            // Coloca animação em idle
            _bubbleObject.GetComponentInChildren<Animator>().SetBool("Walk", false);
            _bubbleObject.GetComponentInChildren<Animator>().SetBool("Jump", false);
            _bubbleObject.GetComponentInChildren<Animator>().SetBool("idle", true);

            // Desativa os movimentos
            _bubbleObject.GetComponent<PlayerController>().enabled = false;
            _bubbleObject.GetComponent<DoubleJump>().enabled = false;

            StartCoroutine(ActivateBlinkingEffect(_gumObject));
        }
        else
        {
            _bubbleObject.GetComponent<PlayerController>().enabled = true; // Ativa o movimento
            _bubbleObject.GetComponent<DoubleJump>().enabled = true; // Ativa o spawn de plataforma com pulo duplo

            UpdateMaterial(_bubbleObject, defaultMaterial); // Define o material ativo
        }
    }

    private void ChangeActivedInGum()
    {
        if (characterControlMode == 2)
        {
            _gumObject.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;

            // Coloca a animação em idle
            _gumObject.GetComponentInChildren<Animator>().SetBool("Walk", false);
            _gumObject.GetComponentInChildren<Animator>().SetBool("Jump", false);
            _gumObject.GetComponentInChildren<Animator>().SetBool("idle", true);

            // Desativa os movimentos
            _gumObject.GetComponent<PlayerController>().enabled = false;
            _gumObject.GetComponent<SplitInHalf>().enabled = false;

            StartCoroutine(ActivateBlinkingEffect(_bubbleObject));
        }
        else
        {
            _gumObject.GetComponent<PlayerController>().enabled = true; // Ativa o movimento
            _gumObject.GetComponent<SplitInHalf>().enabled = true; // Ativa a divisão com spawn de plataforma

            UpdateMaterial(_gumObject, defaultMaterial); // Define o material ativo
        }
    }



    private IEnumerator ActivateBlinkingEffect(GameObject obj)
    {
        SpriteRenderer spriteRenderer = obj.GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            for (int i = 0; i < 2; i++) // Pisca 5 vezes
            {
                UpdateMaterial(obj, activeMaterial); // Ativa o material de brilho
                yield return new WaitForSeconds(0.2f);
                UpdateMaterial(obj, defaultMaterial); // Retorna ao material padrão
                yield return new WaitForSeconds(0.2f);
            }
            UpdateMaterial(obj, defaultMaterial); // Finaliza com o material ativo
        }
    }

    private void UpdateMaterial(GameObject obj, Material newMaterial)
    {
        // Atualiza o material do SpriteRenderer
        SpriteRenderer spriteRenderer = obj.GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.material = newMaterial;
        }
        else
        {
            Debug.LogWarning($"SpriteRenderer não encontrado no filho de {obj.name}.");
        }
    }

}
