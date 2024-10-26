using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour
{
    private PlayerMovement movement;
    private Health health;

    [SerializeField] private float fallilngTime;
    [SerializeField] private float minFallingTime;
    [SerializeField] private float maxFallingTime;
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        health = GetComponent<Health>();
    }

    void Update()
    {
        if (!movement.isClimbing && !movement.isGrounded)
        {
            fallilngTime += Time.deltaTime;
        }
        else
        {
            if (fallilngTime >= minFallingTime)
            {
                if(fallilngTime >= maxFallingTime)
                {
                    health.Die();
                }
                else
                {
                    health.LoseHealth((fallilngTime / maxFallingTime) * 100);
                }
            }
            fallilngTime = 0;
        }
    }   
}
