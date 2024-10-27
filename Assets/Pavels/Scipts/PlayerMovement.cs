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
    [SerializeField] private GameObject bodyCenter;
    [SerializeField] private GameObject firstSpawnPoint;

    [SerializeField] private bool ifCanClimb;

    private Vector2 _moveDirection;

    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public LayerMask hardWallLayer;

    AudioManager audioManager;

    [Space(10)]
    [Header("Only look")]
    [SerializeField] private GameObject lastTouchedObject;
    [SerializeField] private GameObject touchingObject;
    [SerializeField] private GameObject lastRespawnPoint;

    public bool isGrounded;
    public bool isClimbing;
    public bool needsLandSound = false;
    [SerializeField] private bool jumpedOfIce;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        lastRespawnPoint = firstSpawnPoint;
        rb = GetComponent<Rigidbody2D>();
        render = rb.GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feet.transform.position, 0.2f, groundLayer);
        isClimbing = Physics2D.OverlapCircle(transform.position, 0.2f, wallLayer);
        _moveDirection.x = Input.GetAxisRaw("Horizontal");
        _moveDirection.y = Input.GetAxisRaw("Vertical");
        RaycastHit2D hit = Physics2D.Raycast( bodyCenter.transform.position, new Vector2(_moveDirection.x, 0), 0.5f, hardWallLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, new Vector2(_moveDirection.x, 0), 0.5f, hardWallLayer);
        RaycastHit2D hit3 = Physics2D.Raycast(feet.transform.position, new Vector2(_moveDirection.x, 0), 0.5f, hardWallLayer);
        if (hit.collider != null || hit2.collider != null || hit3.collider != null)
        {
            print("Too near!");
            _moveDirection.x = 0;
            print("" + _moveDirection);
        }
        Debug.DrawRay(bodyCenter.transform.position, new Vector2(_moveDirection.x, 0), Color.green, 0.5f);
        Debug.DrawRay(transform.position, new Vector2(_moveDirection.x, 0), Color.green, 0.5f);
        Debug.DrawRay(feet.transform.position, new Vector2(_moveDirection.x, 0), Color.green, 0.5f);
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
        rb.velocity = new Vector2(_moveDirection.x * moveSpeed, rb.velocity.y);

        if (rb.velocity.x > 0)
        {
            render.flipX = false;
        }
        else if (rb.velocity.x < 0)
        {
            render.flipX = true;
        }

        if (rb.velocity.x != 0 && !isClimbing && isGrounded) audioManager.PlayLoopSFX(audioManager.walk);

        if (isClimbing)
        {
            if(_moveDirection.y == 0)
            {
                animator.SetBool("ClimbIdle", true);
                animator.SetBool("isClimbing", false);
                animator.SetBool("isSliding", false);
                audioManager.PlayLoopSFX(audioManager.slide);
            }
            if(_moveDirection.y > 0)
            {
                //animator.SetBool("ClimbIdle", false);
                animator.SetBool("isClimbing", true);
            }
            if(_moveDirection.y < 0)
            {
                //animator.SetBool("ClimbIdle", false);
                audioManager.PlayLoopSFX(audioManager.slideFast);
                animator.SetBool("isSliding", true);
            }
        }
        if(!isClimbing)
        {
            animator.SetBool("ClimbIdle", false);
            animator.SetBool("isClimbing", false);
            animator.SetBool("isSliding", false);
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
                audioManager.PlaySFX(audioManager.jump);
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
                audioManager.PlaySFX(audioManager.jump);
                StartCoroutine(PlayLandingSound());
            }
        }
        if(needsLandSound && isGrounded)
        {
            audioManager.PlaySFX(audioManager.land);
            needsLandSound = false;
        }
    }

    IEnumerator PlayLandingSound ()
    {                 
        yield return new WaitForSeconds (0.05f);
        needsLandSound = true;
    }

    public void MoveToLastSaving()
    {
        transform.position = lastRespawnPoint.transform.position;
    }


    //this is an animation event
    public void SetJumpToFalse()
    {
        animator.SetBool("isJumping", false);
    }
}
