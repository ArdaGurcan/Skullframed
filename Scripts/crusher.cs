using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crusher : MonoBehaviour
{
    float spd = 5;
    public Transform player;
    public Rigidbody2D rb;
    public bool isLocked;
    [SerializeField]
    float eyeSensitivity = 10f;
    [SerializeField]
    Vector2 eyeBall = new Vector2(0.125f, 0.1f);
    bool isBoss;
    Transform eye;
    bool waiting = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        isBoss = gameObject.name == "Boss";
        if (isBoss)
        {
            eye = transform.GetChild(0);
            //spd -= 2;
        }
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (isBoss)
        {
            Vector3 eyePos = Vector3.ClampMagnitude(player.transform.position - transform.position, eyeSensitivity) / eyeSensitivity;
            eye.position = transform.position + new Vector3(eyePos.x * eyeBall.x, eyePos.y * eyeBall.y);
        }
        if (!isLocked)
        {
            if (Mathf.Abs(transform.position.x - player.position.x) < transform.localScale.x / 2 + 0.5f && Mathf.Abs(transform.position.y - player.position.y) <= 5)
            {
                waiting = true;
                isLocked = true;
                if (player.position.y > transform.position.y)
                {
                    StartCoroutine("Move", new Vector3(0, spd, 0));
                }
                else if (player.position.y < transform.position.y)
                {
                    StartCoroutine("Move", new Vector3(0, -spd, 0));
                }
            }
            if (Mathf.Abs(transform.position.y - player.position.y) < transform.localScale.y / 2 + 0.5f && Mathf.Abs(transform.position.x - player.position.x) <= 5)
            {
                waiting = true;
                isLocked = true;
                if (player.position.x > transform.position.x)
                {
                    StartCoroutine("Move", new Vector3(spd, 0, 0));
                }
                else if (player.position.x < transform.position.x)
                {
                    StartCoroutine("Move", new Vector3(-spd, 0, 0));
                    //rb.velocity = (new Vector3(-spd, 0, 0));
                }
            }

        }
        else if (!waiting && rb.velocity.sqrMagnitude < spd * spd * 0.9f)
        {
            isLocked = false;
        }
    }

    IEnumerator Move(Vector3 speed)
    {
        
        yield return new WaitForSeconds(0.3f);
        rb.velocity = speed;
        waiting = false;
    }
    //void OnCollisionEnter2D(Collision2D col)
    //{
    //    rb.velocity = new Vector3(0,0,0);
    //    if (col.gameObject.tag != "Player")
    //        isLocked = false;
    //}
}
