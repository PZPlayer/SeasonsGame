using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RespawnMenu : MonoBehaviour
{
    [SerializeField] private GameObject GameOverMenu;
    [SerializeField] private TMP_Text text;
    [SerializeField] private string[] textAfterDeath;
    Health health;
    int random;

    private void Start()
    {
        random = Random.Range(0, textAfterDeath.Length - 1);
        health = GameObject.FindObjectOfType<Health>();
    }

    public void ShowDeathPanel()
    {
        GameOverMenu.SetActive(true);
        text.text = textAfterDeath[random];
    }

    private void Update()
    {
        if (GameOverMenu.activeInHierarchy && Input.anyKey)
        {
            health.Respawn();
            random = Random.Range(0, textAfterDeath.Length);
            GameOverMenu.SetActive(false);
        }
    }
}
