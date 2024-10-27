using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject spawnPoint;
    Animator anmtr;
    private void Start()
    {
        anmtr = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anmtr.SetTrigger("tch");
            other.GetComponent<PlayerMovement>().lastRespawnPoint = spawnPoint;
            Destroy(this);
        }
    }
}
