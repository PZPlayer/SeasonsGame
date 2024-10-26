using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed, rotSpeed, pushForce;
    [SerializeField] private GameObject crushEffect, bulletRotate;
    void Update()
    {
        bulletRotate.transform.Rotate(new Vector3(0,0,rotSpeed));
        transform.Translate(Vector2.up *  speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<Health>())
        {
            collision.transform.GetComponent<Health>().LoseHealth(45);
            collision.transform.GetComponent<Rigidbody2D>().AddForce(transform.rotation.z == -90 ? Vector2.right * pushForce : Vector2.left * pushForce, ForceMode2D.Impulse);
        }
        GameObject effect = Instantiate(crushEffect, transform.position, transform.rotation);
        Destroy(effect, 2);
        Destroy(gameObject);
    }
}
