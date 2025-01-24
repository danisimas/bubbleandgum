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
        // Verifica se o jogador pode spawnar plataforma
        if (playerController == null || playerController.IsDead) return;
        // Se o jogador pressionar o botão de de se dividir cria ou reposiciona a plataforma
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Z)) SpawnOrReusePlatform();
    }

    private void SpawnOrReusePlatform()
    {
        Vector3 spawnPosition = Vector3.zero;
        // Calcula a posição onde a plataforma será spawnada
        if (transform.localScale.x >= 0) spawnPosition = new Vector3(
            transform.position.x - platformSpawnRadius, transform.position.y, transform.position.z);
        else spawnPosition = new Vector3(
            transform.position.x + platformSpawnRadius, transform.position.y, transform.position.z);

        // Verifica se há colisão com o chão na posição de spawn
        if (Physics2D.OverlapCircle(spawnPosition, platformSpawnRadius, groundLayer) != null)
        {
            Debug.Log("Não é possível spawnar a plataforma: Local ocupado pelo chão.");
            return; // Bloqueia o spawn da plataforma
        }

        if (currentPlatform == null)
        {
            // Instancia a plataforma
            currentPlatform = Instantiate(platformPrefab);

            // Reposiciona a plataforma ao lado em que o jogador está virado
            currentPlatform.transform.position = spawnPosition;

            Debug.Log("Plataforma Instanciada");

            // reduz o tamanho do personagem pela metade
            transform.localScale = new Vector3(transform.localScale.x / 2f, transform.localScale.y / 2, transform.localScale.z);
        }
        else
        {
            // Destrói a plataforma
            Destroy(currentPlatform);
            // Retorna o personagem a seu tamanho original
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
