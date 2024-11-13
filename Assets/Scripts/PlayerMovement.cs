using ReviewBonus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Subject
{
    private float horizontal;
    private float speed = 8f;
    private float jumpPower = 16f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    public int score;

    private UIManager uiManager;
    private void Awake()
    {
        uiManager = (UIManager)FindObjectOfType(typeof(UIManager));
    }

    private void Start()
    {
        
        score = 0;    
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        NotifyObservers();
    }

    private void OnEnable()
    {
        Attach(uiManager);
    }

    private void OnDisable()
    {
        Detach(uiManager);
    }

    private void Move()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            score += collision.GetComponent<Objectives>().RewardScore();
            gameObject.SetActive(false);
        }
    }
}
