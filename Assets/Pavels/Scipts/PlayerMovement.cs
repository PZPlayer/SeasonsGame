using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Can Change")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float climbSpeed = 3f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer render;

    [SerializeField] private GameObject feet;
    

    [SerializeField] private bool ifCanClimb;

    private Vector2 _moveDirection;

    public LayerMask groundLayer;
    public LayerMask wallLayer;

    [Space(10)]
    [Header("Only look")]
    [SerializeField] private GameObject lastTouchedObject;
    [SerializeField] private GameObject touchingObject;

    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isClimbing;
    [SerializeField] private bool jumpedOfIce;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        render = rb.GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feet.transform.position, 0.2f, groundLayer);
        isClimbing = Physics2D.OverlapCircle(transform.position, 0.2f, wallLayer);

        if (isClimbing && touchingObject == null)
        {
            touchingObject = Physics2D.OverlapCircle(transform.position, 0.2f, wallLayer).gameObject;
        }
        else if (!isClimbing && touchingObject != null)
        {
            lastTouchedObject = null;
            touchingObject = null;
        }
        if (isGrounded)
        {
            lastTouchedObject = null;
            jumpedOfIce = false;
        }

        Move();
    }

    void Move()
    {
        _moveDirection.x = Input.GetAxisRaw("Horizontal");
        _moveDirection.y = Input.GetAxisRaw("Vertical");
        if(!jumpedOfIce) rb.velocity = new Vector2(_moveDirection.x * moveSpeed, rb.velocity.y);

        if (rb.velocity.x > 0)
        {
            render.flipX = false;
        }
        else if (rb.velocity.x < 0)
        {
            render.flipX = true;
        }

        //if walking animation walk
        animator.SetBool("isWalking", Input.GetAxisRaw("Horizontal") != 0 && isGrounded ? true : false);

        if (isClimbing && touchingObject != lastTouchedObject)
        {
            rb.velocity = new Vector2(_moveDirection.x * climbSpeed, ifCanClimb == true ? _moveDirection.y * climbSpeed : Mathf.Clamp(_moveDirection.y * climbSpeed, -climbSpeed, 0.2f));
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(new Vector2(_moveDirection.x * jumpForce / 2, 1 * jumpForce), ForceMode2D.Impulse);
                lastTouchedObject = touchingObject;
                jumpedOfIce = true;
                animator.SetBool("isJumping", true);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                animator.SetBool("isJumping", true);
            }
        }
    }


    //this is an animation event
    public void SetJumpToFalse()
    {
        animator.SetBool("isJumping", false);
    }
}