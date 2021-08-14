using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyingEnemy : MonoBehaviour
{
    public float spd;
    public Transform player;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.position.x > transform.position.x && Mathf.Abs(transform.position.x - player.position.x) <= 5){
		rb.velocity = (new Vector2(rb.velocity.x+spd,rb.velocity.y));
		transform.localScale = new Vector3(1,1,1);
	} else if(player.position.x < transform.position.x && Mathf.Abs(transform.position.x - player.position.x) <= 5){
		rb.velocity = (new Vector2(rb.velocity.x-spd,rb.velocity.y));
		transform.localScale = new Vector3(-1,1,1);
	}
        if(player.position.y > transform.position.y && Mathf.Abs(transform.position.y - player.position.y) <= 5){
		rb.velocity = (new Vector2(rb.velocity.x,rb.velocity.y+spd));
	} else if(player.position.y < transform.position.y && Mathf.Abs(transform.position.y - player.position.y) <= 5){
		rb.velocity = (new Vector2(rb.velocity.x,rb.velocity.y-spd));
	}
    }
}
