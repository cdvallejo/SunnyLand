using UnityEngine;

public class pigCtrl : MonoBehaviour
{
    [SerializeField] private Transform[] waypoint;
    [SerializeField] private float speed = 0.01f;
    // Range hace que aparezca una barra deslizante en el inspector
    [Range(0.01f, 1)] public float timeToDeath = 0.35f;
    private int currentWaypointIndex = 0;
    private bool dead = false;
    private SpriteRenderer spriteRenderer;
    public AudioSource audioJumpHit; 
    /*[SerializeField]*/ public bool isMoveLeft = false;

    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (animator == null) {
            animator = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    private void Update() {
        if(!dead)
            if (waypoint.Length > 0) {
                Transform currentWaypointTransform = waypoint[currentWaypointIndex];
                transform.position = Vector2.MoveTowards(transform.position, currentWaypointTransform.position, speed);
            }   
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("waypoint")) {
           Flip(); 
        }

    }

    public void Flip() {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoint.Length;
        isMoveLeft = !isMoveLeft;
        spriteRenderer.flipX = isMoveLeft;
    }


    public void Death() {
        audioJumpHit.Play();
        dead = true;
        //Destroy(gameObject);
        animator.SetTrigger("Death");
        //speed = 0;
        Destroy(gameObject, timeToDeath); // Destruye el objeto después de 1 segundo
    }

    public bool IsDead() {
        return dead;
    }
}