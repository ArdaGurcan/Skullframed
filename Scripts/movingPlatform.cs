using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    public int forward;
    public int dir;
    public int movement;
    public float spd;
    public Rigidbody2D rb;
    public bool isVertical;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = forward;
        dir = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(movement > 0){
            movement -= 1;
        } else if(movement < 0){
            movement += 1;
        }
        if(movement == 0){
            forward *= -1;
            dir *= -1;
            movement = forward;
        }
        if(isVertical){
            rb.velocity = new Vector3(0,dir*spd,0);
        } else {
            rb.velocity = new Vector3(dir*spd,0,0);
        }
    }
}
