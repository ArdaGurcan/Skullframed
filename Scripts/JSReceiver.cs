using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

public class JSReceiver : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern float GetX();
    [DllImport("__Internal")]
    private static extern float GetY();
    [DllImport("__Internal")]
    private static extern void SendY(float y);
    [DllImport("__Internal")]
    private static extern void Enable();

    Rigidbody2D rb;
    Vector3 startPos;
    bool wasDisabled;
    // Start is called before the first frame update

    void Start()
    {
        startPos = transform.position;
        Application.targetFrameRate = 300;
        rb = GetComponent<Rigidbody2D>();
        //Move("0~-50");
        wasDisabled = SceneManager.GetActiveScene().buildIndex == 0;
    }

    // Update is called once per frame
   
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex != 0 || GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().fallen)

        {
            if(wasDisabled)
            {
                Enable();
                wasDisabled = false;
            }
            rb.MovePosition(new Vector3(startPos.x + GetX(), startPos.y + GetY(), transform.position.z));
        }
        else
        {
            SendY(-transform.position.y);
        }
    }

    //public void Move(string str)
    //{
    //    Debug.Log(float.Parse(str.Split('~')[0]));
    //    Debug.Log(float.Parse(str.Split('~')[1]));
    //    rb.MovePosition(new Vector3(float.Parse(str.Split('~')[0]), float.Parse(str.Split('~')[1]), transform.position.z));
    //    //transform.position = new Vector3(float.Parse(str.Split('~')[0]), float.Parse(str.Split('~')[1]),transform.position.z);

    //}
}
