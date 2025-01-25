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
        Debug.Log(GameController.Instance._gameIsPaused);
        Debug.Log(GameController.Instance.isGameOver);

        if ((GameController.Instance._gameIsPaused == false) && (GameController.Instance.isGameOver == false))

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

}


