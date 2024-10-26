using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Events;
using UnityEngine.Events;
using System.ComponentModel;

public class Health : MonoBehaviour
{
    private Animator anmtr;

    public float MaxHealth;
    public float CurHealth;

    [Header("Events")]
    [Description("You can add here functions, that will be called once health changes")]
    public UnityEvent loseHealth;
    public UnityEvent gainHealth;
    public UnityEvent Death;
    public UnityEvent OnRespawn;

    private void Awake()
    {
        anmtr = GetComponent<Animator>();
    }

    private void Update()
    {
        if(CurHealth <= 0)
        {
            Die();
        }
    }

    public void LoseHealth(float health)
    {
        loseHealth.Invoke();
        CurHealth -= health;
    }

    public void GainHealth(float health)
    {
        gainHealth.Invoke();
        CurHealth += health;
    }

    public void Die()
    {
        anmtr.SetTrigger("Death");
        Death.Invoke();
    }

    public void Respawn()
    {
        CurHealth = MaxHealth;
        anmtr.SetTrigger("Revived");
        anmtr.ResetTrigger("Death");
        OnRespawn.Invoke();
    }
}
