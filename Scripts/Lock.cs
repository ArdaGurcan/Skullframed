using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public GameObject[] keys;
    bool locked = true;
    public Sprite unlocked;
    
    void Update()
    {
        if(locked)
        {
            locked = false;
            for (int i = 0; i < keys.Length; i++)
            {
                if(keys[i].transform.GetChild(0).gameObject.activeInHierarchy)
                {
                    locked = true;
                    break;
                }
            }
            if(!locked)
            {
                GetComponent<SpriteRenderer>().sprite = unlocked;
                GetComponents<BoxCollider2D>()[0].enabled = false;
                GetComponents<BoxCollider2D>()[1].enabled = false;
            }
        }
    }
}
