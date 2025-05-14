using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private TextMeshProUGUI gemsCountText;
    [SerializeField] private TextMeshProUGUI lifesCountText;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public AudioSource audioJump;
    public AudioSource audioGem;
    public AudioSource audioJumpHit;

    private bool isGrounded = false;
    private int gemsCount = 0;
    [SerializeField] private int lifesCount = 3;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        gemsCountText.text = gemsCount.ToString();
        lifesCountText.text = lifesCount.ToString();
    }

    // Update se llama una vez por frame
    void Update()
    {
        // La animación de correr se activa si la velocidad en el eje X es diferente de 0
        animator.SetBool("isRunning", Math.Abs(rb.linearVelocityX) > 0.2);
        // Si el jugador toca la pantalla con dos dedos, salta
        if (Input.touchCount > 1) Jump();
        // Si el jugador toca la pantalla con un dedo, se mueve a la izquierda o derecha dependiendo de la posición del toque
        if (Input.touchCount > 0)
        {
            UnityEngine.Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Stationary)
            {
                Move(touch);
            }
        }
        
        //LevelManager.instance.UpdateStats(gemsCount, lifesCount);

#if UNITY_EDITOR
        PlayerMovement();
#endif
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
            audioJump.Play();
        }
    }

    // Hemos congelado el eje Z en el componente Ribibody para que no haga la croqueta moviéndose
    private void Move(UnityEngine.Touch t)
    {

        if (t.position.x > Screen.width / 3)
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocityY);
            spriteRenderer.flipX = false;
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocityY);
            // Rotamos el idle cuando va hacia la izquierda
            spriteRenderer.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Confirma que el jugador está en el suelo
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }
        //}

        // Stay se llama una vez el colisionador ya está chocando
        //private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("enemy"))
        {
            if (collision.gameObject.GetComponent<pigCtrl>() != null)
            {
                if (collision.gameObject.GetComponent<pigCtrl>().IsDead() == false)
                {

                    // Si el jugador colisiona con un enemigo pierde una vida
                    animator.SetTrigger("isHit");
                    if (collision.gameObject.GetComponent<pigCtrl>() != null)
                    {
                        collision.gameObject.GetComponent<pigCtrl>().Flip();
                    }
                    if (lifesCount >= 1)
                    {
                        lifesCount--;
                        lifesCountText.text = lifesCount.ToString();

                    } else {
                        // Si el jugador no tiene vidas, se destruye
                        LevelManager.instance.ShowGameOver();
                    }
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Confirma que el jugador no está en el suelo
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("gem"))
        {
            // Si el jugador colisiona con una gema, suena y se destruye
            audioGem.Play();
            Destroy(collision.gameObject, 0.5f);  
            gemsCount++;
            // Se actualiza el texto de la UI con el número de gemas recogidas
            gemsCountText.text = gemsCount.ToString();
        }

    }

    // Sólo para probar el juego mediante teclado en el editor, ya que Unity Remote falla mucho...
    public void PlayerMovement()
    {
        // salto
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        float modID = Input.GetAxisRaw("Horizontal");
        rb.linearVelocityX = speed * modID;
        if (rb.linearVelocityX > 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }

    }
}
