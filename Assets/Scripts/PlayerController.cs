using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimentação")]
    public float moveSpeed = 5f; // Velocidade de movimento horizontal
    public float jumpForce = 10f; // Força do pulo
    private float horizontalInput; // Entrada horizontal
    private bool isGrounded; // Indica se o jogador está no chão
    private bool isJumping; // Impede múltiplos pulos no ar

    public bool IsGrounded => isGrounded; // Propriedade pública somente leitura para isGrounded
    public bool IsDead => isDead;         // Propriedade pública somente leitura para isDead


    [Header("Componentes")]
    public Rigidbody2D rb; // Referência ao Rigidbody2D
    public LayerMask groundLayer; // Layer que representa o chão
    private Animator animator; // Referência ao Animator para controlar animações
    private Collider2D playerCollider; // Collider do jogador

    private bool isDead; // Indica se o jogador está morto

    private void Start()
    {
        // Obtém os componentes necessários
        animator = GetComponentInChildren<Animator>();
        playerCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (isDead) return; // Se está morto, não faz nada

        GetInput();        // Captura a entrada do jogador
        HandleMovement();  // Gerencia o movimento horizontal
        HandleJump();      // Gerencia o pulo
        HandleAnimations();// Atualiza as animações
    }

    // Captura os inputs do jogador
    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal"); // Entrada para movimento lateral
    }

    // Gerencia o movimento horizontal do jogador
    private void HandleMovement()
    {
        // Aplica a velocidade no eixo X enquanto mantém a velocidade atual no eixo Y
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        // Ajusta a direção do sprite baseado na entrada horizontal
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)))
            if (transform.localScale.x > 0) transform.localScale = new Vector3(
                -transform.localScale.x, transform.localScale.y, 1); // se a escala local estiver virada para a direita, a inverte

        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)))
            if (transform.localScale.x < 0) transform.localScale = new Vector3(
                -transform.localScale.x, transform.localScale.y, 1);// se a escala local estiver virada para a esquerda, a inverte


    }

    // Gerencia o pulo do jogador
    private void HandleJump()
    {
        // Inicia o pulo se pressionar a tecla de pulo e estiver no chão
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded && !isJumping)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Aplica força no eixo Y
            isJumping = true; // Marca como "no ar" para evitar múltiplos pulos
        }


    }


    // Atualiza as animações baseadas nos estados do jogador
    private void HandleAnimations()
    {
        animator.SetBool("isGround", isGrounded); // Define se está no chão
        animator.SetBool("Jump", isJumping && !isGrounded); // Animação de pulo
        animator.SetBool("Walk", Mathf.Abs(horizontalInput) > 0 && isGrounded); // Animação de andar
        animator.SetBool("idle", isGrounded && horizontalInput == 0); // Animação de idle (parado)
    }

    // Método chamado para "matar" o jogador
    public void Die()
    {
        if (isDead) return; // Evita que o método seja chamado múltiplas vezes

        isDead = true; // Marca como morto

        // Ativa a animação de morte
        animator.SetBool("Death", true);

        // Desativa o movimento do jogador
        rb.linearVelocity = Vector2.zero; // Para o movimento imediatamente
        rb.isKinematic = true; // Torna o Rigidbody kinematic para impedir forças físicas

        // Opcional: Desativa o controle do jogador após a morte
        this.enabled = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o objeto colidido tem a tag "Danger"
        if (collision.gameObject.CompareTag("Danger"))
        {
            Die();
            GameController.Instance.LoseLife(gameObject.tag);
        }
    }




    // Detecta quando o jogador entra em contato com o chão
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((((1 << collision.gameObject.layer) & LayerMask.GetMask("Ground")) != 0)
        || collision.gameObject.CompareTag("Player")) // Verifica se o objeto está no Layer do chão
        {
            isGrounded = true;
            isJumping = false;
        }


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((((1 << collision.gameObject.layer) & LayerMask.GetMask("Ground")) != 0)
        || collision.gameObject.CompareTag("Player")) // Verifica se o objeto está no Layer do chão
        {
            isGrounded = true;
            isJumping = false;
        }


    }

    // Detecta quando o jogador sai do contato com o chão
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((((1 << collision.gameObject.layer) & LayerMask.GetMask("Ground")) != 0)
        || collision.gameObject.CompareTag("Player")) // Verifica se o objeto está no Layer do chão
        {
            isGrounded = false;
        }
    }
}
