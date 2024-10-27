using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public GameObject StartGame;

    public void Awake()
    {
        StartGame.SetActive(true);
        StartCoroutine(remmoveMessage());
    }

    
    IEnumerator remmoveMessage()
    {
        yield return new WaitForSecondsRealtime(4f);
            StartGame.SetActive(false);
    }

}
