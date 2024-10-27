using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugScript : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    [SerializeField] private float speed = 2f;
    [SerializeField] private float force = 10f; 

    private Vector3 targetPosition; 
    private bool isMovingToPointA = true; 
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPosition = pointA.position; 
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMovingToPointA = !isMovingToPointA;
            targetPosition = isMovingToPointA ? pointA.position : pointB.position;
        }

        Vector2 direction = (targetPosition - transform.position).normalized;
        rb.velocity = direction * speed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 direction = (collision.gameObject.transform.position - transform.position).normalized;
            playerRb.AddForce(direction * force, ForceMode2D.Impulse);
        }
    }
}
