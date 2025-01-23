using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    private PlayerController playerController; // Referência ao script principal de controle
    private bool canDoubleJump; // Indica se o jogador pode realizar o pulo duplo

    private void Start()
    {
        // Obtém a referência do PlayerController anexado a este objeto
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        // Verifica se o jogador pode pular
        if (playerController == null || playerController.IsDead) return;

        // Se o jogador estiver no ar e pressionar o botão de pulo
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && !playerController.IsGrounded)
        {
            // Realiza o pulo duplo se disponível
            if (canDoubleJump)
            {
                PerformDoubleJump();
                canDoubleJump = false; // Desativa o pulo duplo após usá-lo
            }
        }

        // Reseta o estado do pulo duplo ao tocar no chão
        if (playerController.IsGrounded)
        {
            canDoubleJump = true;
        }
    }

    private void PerformDoubleJump()
    {
        // Aplica uma nova velocidade vertical para simular o pulo duplo
        playerController.rb.linearVelocity = new Vector2(playerController.rb.linearVelocity.x, playerController.jumpForce);
        Debug.Log("Pulo Duplo Realizado!");
    }
}
