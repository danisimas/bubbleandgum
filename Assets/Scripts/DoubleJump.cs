using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    private PlayerController playerController; // Referência ao script principal de controle
    private bool canDoubleJump; // Indica se o jogador pode realizar o pulo duplo
    public GameObject platformPrefab; // Prefab da plataforma a ser instanciada
    private GameObject currentPlatform; // Referência à plataforma atual (reutilizável)
    public LayerMask groundLayer; // Layer que representa o chão
    public float platformSpawnRadius = 0.5f; // Raio para verificar sobreposição com o chão e spawnar plataforma

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
                SpawnOrReusePlatform(); // Cria ou reposiciona a plataforma
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
        playerController.rb.velocity = new Vector2(playerController.rb.velocity.x, playerController.jumpForce);
        Debug.Log("Pulo Duplo Realizado!");
    }

    private void SpawnOrReusePlatform()
    {
        // Calcula a posição onde a plataforma será spawnada
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y - platformSpawnRadius, transform.position.z);

        // Verifica se há colisão com o chão na posição de spawn
        if (Physics2D.OverlapCircle(spawnPosition, platformSpawnRadius, groundLayer) != null)
        {
            Debug.Log("Não é possível spawnar a plataforma: Local ocupado pelo chão.");
            return; // Bloqueia o spawn da plataforma
        }

        if (currentPlatform == null)
        {
            // Instancia a plataforma se ainda não existir
            currentPlatform = Instantiate(platformPrefab);
            Debug.Log("Plataforma Instanciada");
        }

        // Reposiciona a plataforma abaixo do jogador
        currentPlatform.transform.position = spawnPosition;
        Debug.Log("Plataforma Reutilizada ou Criada");
    }

    private void OnDrawGizmosSelected()
    {
        // Desenha um gizmo para visualizar o raio de verificação
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position - new Vector3(0, platformSpawnRadius, 0), platformSpawnRadius);
    }
}
