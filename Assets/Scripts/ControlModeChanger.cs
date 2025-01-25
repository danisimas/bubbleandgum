using UnityEngine;
using System.Collections;

public class ControlModeChanger : MonoBehaviour
{

    private int characterControlMode = 0;


    private void Update()
    {
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
            GameController.Instance._bubbleObject.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;

            // coloca animação em idle
            GameController.Instance._bubbleObject.GetComponentInChildren<Animator>().SetBool("Walk", false);
            GameController.Instance._bubbleObject.GetComponentInChildren<Animator>().SetBool("Jump", false);
            GameController.Instance._bubbleObject.GetComponentInChildren<Animator>().SetBool("idle", true);

            // desativa os movimentos
            GameController.Instance._bubbleObject.GetComponent<PlayerController>().enabled = false;
            GameController.Instance._bubbleObject.GetComponent<DoubleJump>().enabled = false;
        }
        else
        {
            GameController.Instance._bubbleObject.GetComponent<PlayerController>().enabled = true; // Ativa o movimento
            GameController.Instance._bubbleObject.GetComponent<DoubleJump>().enabled = true; // Ativa o spawn de plataforma com pulo duplo
        }

    }

    private void ChangeActivedInGum()
    {
        if (characterControlMode == 2)
        {
            GameController.Instance._gumObject.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;

            // coloca a animação em idle
            GameController.Instance._gumObject.GetComponentInChildren<Animator>().SetBool("Walk", false);
            GameController.Instance._gumObject.GetComponentInChildren<Animator>().SetBool("Jump", false);
            GameController.Instance._gumObject.GetComponentInChildren<Animator>().SetBool("idle", true);

            // desativa os movimentos
            GameController.Instance._gumObject.GetComponent<PlayerController>().enabled = false;
            GameController.Instance._gumObject.GetComponent<SplitInHalf>().enabled = false;
        }
        else
        {
            GameController.Instance._gumObject.GetComponent<PlayerController>().enabled = true; // Ativa o movimento
            GameController.Instance._gumObject.GetComponent<SplitInHalf>().enabled = true; // Ativa a divisão com spawn de plataforma
        }
    }

}


