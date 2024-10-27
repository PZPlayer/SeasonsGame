using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float reloadTime;
    private Animator anmtr;
    private AudioSource source;
    float timer;

    private void Start()
    {
        anmtr = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if( timer > reloadTime)
        {
            anmtr.SetTrigger("Shoot");
            source.Play();
            Invoke("Shoot", 0.2f); //0.2 it's a time in animation when fire comes out of a canon
            timer = 0;
        }
    }

    void Shoot()
    {
        GameObject gameObject = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        Destroy(gameObject, 5f);
    }
}
