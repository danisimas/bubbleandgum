using UnityEngine;

public class ControlModeChanger : MonoBehaviour
{
    private int characterControlMode = 0;

    [SerializeField] private GameObject _bubbleObject;
    [SerializeField] private GameObject _gumObject;

    [Header("Materials")]
    [SerializeField] private Material activeMaterial;   // Material para objeto ativo
    [SerializeField] private Material inactiveMaterial; // Material para objeto inativo

    private void Update()
    {
        if (!GameController.Instance._gameIsPaused && !GameController.Instance.isGameOver)
        {
            ChangeCharacterControl();
        }
    }

    private void ChangeCharacterControl()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            // Alterna entre os modos de controle (0, 1 e 2)
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
            // Configuração para Bubble inativo
            _bubbleObject.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;

            // Ajusta animações para idle
            var animator = _bubbleObject.GetComponentInChildren<Animator>();
            animator.SetBool("Walk", false);
            animator.SetBool("Jump", false);
            animator.SetBool("idle", true);

            // Desativa os movimentos
            _bubbleObject.GetComponent<PlayerController>().enabled = false;
            _bubbleObject.GetComponent<DoubleJump>().enabled = false;

            // Atualiza o material para inativo
            UpdateMaterial(_bubbleObject, inactiveMaterial);
        }
        else
        {
            // Configuração para Bubble ativo
            _bubbleObject.GetComponent<PlayerController>().enabled = true;
            _bubbleObject.GetComponent<DoubleJump>().enabled = true;

            // Atualiza o material para ativo
            UpdateMaterial(_bubbleObject, activeMaterial);
        }
    }

    private void ChangeActivedInGum()
    {
        if (characterControlMode == 2)
        {
            // Configuração para Gum inativo
            _gumObject.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;

            // Ajusta animações para idle
            var animator = _gumObject.GetComponentInChildren<Animator>();
            animator.SetBool("Walk", false);
            animator.SetBool("Jump", false);
            animator.SetBool("idle", true);

            // Desativa os movimentos
            _gumObject.GetComponent<PlayerController>().enabled = false;
            _gumObject.GetComponent<SplitInHalf>().enabled = false;

            // Atualiza o material para inativo
            UpdateMaterial(_gumObject, inactiveMaterial);
        }
        else
        {
            // Configuração para Gum ativo
            _gumObject.GetComponent<PlayerController>().enabled = true;
            _gumObject.GetComponent<SplitInHalf>().enabled = true;

            // Atualiza o material para ativo
            UpdateMaterial(_gumObject, activeMaterial);
        }
    }

    private void UpdateMaterial(GameObject obj, Material newMaterial)
    {
        // Altera o material do objeto
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = newMaterial;
        }
    }
}
