using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class PlayerScript : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void Reset();
    [DllImport("__Internal")]
    private static extern void Enable();

    public Sprite[] sprites;
    int rot = 0;
    int lastRot = 0;
    SpriteRenderer sr;
    bool jump = false;
    bool jumpUsed = false;

    public bool fallen = false;
    [SerializeField]
    float height;
    AudioSource audioSource;

    [SerializeField]
    AudioClip keySound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            Reset();
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
        }
        if(!fallen && transform.position.y  >= 8f)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
            Camera.main.GetComponent<CircleCollider2D>().enabled = false;
            GameObject[] arrows = GameObject.FindGameObjectsWithTag("Respawn");
            for (int i = 0; i < arrows.Length; i++)
            {
                arrows[i].GetComponent<PolygonCollider2D>().isTrigger = false;
                arrows[i].GetComponent<Rigidbody2D>().isKinematic = false;
            }
            GameObject[] spikes = GameObject.FindGameObjectsWithTag("Finish");
            for (int i = 0; i < spikes.Length; i++)
            {
                spikes[i].SetActive(false);
            }
            BoxCollider2D[] boxes = Camera.main.GetComponents<BoxCollider2D>();
            boxes[0].enabled = true; boxes[1].enabled = true; boxes[2].enabled = true; boxes[3].enabled = true;
        }
        if(!fallen && transform.position.y < -10)
        {
            fallen = true;
            Enable();
            GameObject.FindGameObjectWithTag("Title").GetComponent<SpriteRenderer>().enabled = true;

            Camera.main.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            Camera.main.GetComponent<Rigidbody2D>().isKinematic = true;
            Camera.main.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        if (!fallen && SceneManager.GetActiveScene().buildIndex == 0 && Input.GetKeyDown(KeyCode.UpArrow))
        {
            jump = true;//GetComponent<Rigidbody2D>().AddForce(Vector3.up * 20f, ForceMode2D.Impulse);
            
        }
        rot = (Mathf.RoundToInt((transform.rotation.eulerAngles.z - 135) / 45) % sprites.Length + sprites.Length) % sprites.Length;
        if(rot!=lastRot)
        {
            sr.sprite = sprites[rot];
            lastRot = rot;
        }
    }


    private void FixedUpdate()
    {
        if (!fallen && SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                GetComponent<Rigidbody2D>().AddForce(Vector3.right * 0.5f * 1000);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                GetComponent<Rigidbody2D>().AddForce(Vector3.right * -0.5f * 1000);
            }
            if (!jumpUsed && jump)
            {
                jump = false;
                jumpUsed = true;
                GetComponent<Rigidbody2D>().AddForce(Vector3.up * height * 1000, ForceMode2D.Impulse);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Death":
                Reset();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case "Finish":
                Reset();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case "Goal":
                Reset();
                SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex+1) % SceneManager.sceneCountInBuildSettings);
                break;
            case "Key":
                collision.gameObject.SetActive(false);
                audioSource.PlayOneShot(keySound);
                break;
            default:
                break;
        }

    }



}
