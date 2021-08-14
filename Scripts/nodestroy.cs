using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nodestroy : MonoBehaviour
{
    [SerializeField]
    AudioClip[] pieces;
    int thisScene = -1;
    // Start is called before the first frame update
    void Start()
    {
        
        DontDestroyOnLoad(gameObject);

	GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }	
    }

    // Update is called once per frame
    void Update()
    {
        //if (SceneManager.GetActiveScene().buildIndex == 0)
        //{
        //    GetComponent<AudioSource>().Stop();
        //    GetComponent<AudioSource>().loop = true;
        //    GetComponent<AudioSource>().PlayOneShot(pieces[0]);

        //}
        //else
        if(SceneManager.GetActiveScene().buildIndex != thisScene)
        {
            thisScene = SceneManager.GetActiveScene().buildIndex;
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            GetComponent<AudioSource>().Stop();

            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().PlayOneShot(pieces[1]);

        }
        else if (SceneManager.GetActiveScene().buildIndex == 25)
        {
            GetComponent<AudioSource>().Stop();

            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().PlayOneShot(pieces[2]);

        }
        }
    }
}
