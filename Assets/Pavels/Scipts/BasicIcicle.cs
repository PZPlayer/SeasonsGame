using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class BasicIcicle : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private GameObject target;
    AudioSource source;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            Health player = other.collider.GetComponent<Health>();
            player.LoseHealth(80);
        }
        source = GetComponent<AudioSource>();
        source.Play();
        transform.GetComponent<SpriteRenderer>().enabled = false;
        transform.GetComponent<BoxCollider2D>().enabled = false;
        Destroy(target.gameObject, 1f);
        print("Crashed at:" + transform.position);
    }
}
