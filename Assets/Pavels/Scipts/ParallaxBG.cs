using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    private float startPos, length;
    public GameObject cam;
    public float parallaxEffect;
    public float travelSpeed = 0;

    void Start()
    {
        startPos = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.y;
        //GetComponent<Renderer>().material.mainTextureOffset = new Vector2((Time.time * travelSpeed)%1, 0f);
    }


    void FixedUpdate()
    {
        float distance = cam.transform.position.y * parallaxEffect; //0 = move with cam or 1 = wont move
        transform.position = new Vector3(transform.position.x, startPos + distance, transform.position.z);
        float movement = cam.transform.position.y * (1- parallaxEffect);

        if(movement > startPos + length)
        {
            startPos += length;
        }
        else if (movement < startPos - length)
        {
            startPos -= length;
        }
    }
}
