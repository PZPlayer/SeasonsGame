using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Icicle : MonoBehaviour
{
    public enum IcicleState
    {
        Appear = 0,
        Normal = 1,
        StartFalling = 2,
        Falling = 3,
        Crashing = 4
    }

    Rigidbody2D rb;
    BoxCollider2D col;
    Animator animatr;
    IcicleState state;
    AudioSource source;

    [SerializeField] private float respawnTime, waitTilFalling = 2, startTime = 0;
    [SerializeField] private int stateInt;
    [SerializeField] private GameObject ice, spawnPoint;
    [SerializeField] private Animation NormalAnim;
    private Vector3 startPos;
    float timer;

    private void Start()
    {
        animatr = GetComponentInParent<Animator>();
        col = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
        transform.GetComponent<SpriteRenderer>().enabled = true;
        transform.GetComponent<BoxCollider2D>().enabled = true;
        rb.simulated = false;
        startPos = transform.position;
        Invoke("Appear", startTime);
    }

    void Appear()
    {
        transform.position = startPos;
        transform.GetComponent<SpriteRenderer>().enabled = true;
        transform.GetComponent<BoxCollider2D>().enabled = true;
        rb.velocity = new Vector3(0,0,0);
        rb.simulated = false;
        state = IcicleState.Appear;
        print("Start Pos:" + transform.position);
        ChangeAnmtrOption();
        Invoke("GoNormal", 0.5f);
    }

    void GoNormal()
    {
        state = IcicleState.Normal;
        ChangeAnmtrOption();
        Invoke("GoStartFalling", waitTilFalling);
    }

    void GoStartFalling()
    {
        state = IcicleState.StartFalling;
        ChangeAnmtrOption();
        Invoke("GoFalling", 0.5f);
    }

    void GoFalling()
    {
        state = IcicleState.Falling;
        Instantiate(ice, spawnPoint.transform.position, spawnPoint.transform.rotation);
        transform.GetComponent<SpriteRenderer>().enabled = false;
        transform.GetComponent<BoxCollider2D>().enabled = false;
        Invoke("Appear", respawnTime);
    }

    void ChangeAnmtrOption()
    {
        stateInt = (int)state;
        animatr.SetInteger("State", (int)state);
    }
}
