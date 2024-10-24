using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float climbSpeed = 3f;

    private Rigidbody2D rb;

    [SerializeField] private GameObject feet;

    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isClimbing;
    [SerializeField] private bool ifCanClimb;
    private Vector2 moveDirection;

    public LayerMask groundLayer;
    public LayerMask wallLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(feet.transform.position, 0.2f, groundLayer);

        isClimbing = Physics2D.OverlapCircle(transform.position, 0.2f, wallLayer);


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (isClimbing)
        {
            rb.velocity = new Vector2(moveDirection.x * climbSpeed, ifCanClimb == true ? moveDirection.y * climbSpeed : Mathf.Clamp(moveDirection.y * climbSpeed, -climbSpeed, 0.2f));
        }
    }
}
