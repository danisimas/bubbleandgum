using UnityEngine;

public class SplitInHalf : MonoBehaviour
{
    private PlayerController playerController; // Referência ao script principal de controle
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
        // Se o jogador pressionar o botão de de se dividir cria ou reposiciona a plataforma
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Z)) SpawnOrReusePlatform();
    }

    private void SpawnOrReusePlatform()
    {
        // Calcula a posição onde a plataforma será spawnada
        Vector3 spawnPosition = new Vector3(transform.position.x - platformSpawnRadius, transform.position.y, transform.position.z);

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
            // Reposiciona a plataforma abaixo do jogador
            currentPlatform.transform.position = spawnPosition;
            Debug.Log("Plataforma Instanciada");
            transform.localScale = new Vector3(transform.localScale.x / 2f, transform.localScale.y / 2, transform.localScale.z);
        }
        else
        {
            Destroy(currentPlatform);
            transform.localScale = new Vector3(transform.localScale.x * 2f, transform.localScale.y * 2, transform.localScale.z);
        }

        Debug.Log("Plataforma Reutilizada ou Criada");

    }

    private void OnDrawGizmosSelected()
    {
        // Desenha um gizmo para visualizar o raio de verificação
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position - new Vector3(platformSpawnRadius, 0, 0), platformSpawnRadius);
    }
}
