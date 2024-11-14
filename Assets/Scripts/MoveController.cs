using UnityEngine;

public class MoveController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    private float xInput;
    private const float BOUNDARY = 8.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameManager.isTutorial && Tutorial.count<4){
            return;
        }
        if (GameManager.isTutorial && Tutorial.count>8){
            return;
        }
        // left/right movement
        xInput = Input.GetAxisRaw("Horizontal");
        Movement();
    }

    private void Movement()
    {
        float currentX = transform.position.x;

        // player plane cannot move across left or right of the screen
        if ((currentX >= BOUNDARY && xInput > 0) || (currentX <= -BOUNDARY && xInput < 0))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }

        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
    }
}