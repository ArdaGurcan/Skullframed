using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaletteSwitcher : MonoBehaviour
{
    [System.Serializable]
    public class Palette
    {
        public Color[] colors;
    }
    public Palette[] palettes;

    public Texture2D[] textures;
    public AudioClip next;
    public AudioClip prev;
    AudioSource audioSource;
    [SerializeField]
    int colorIndex = 0;
    float time = 0.6f;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        for (int i = 0; i < palettes.Length; i++)
        {
            if (textures[0].GetPixel(0, 0) == palettes[i].colors[0])
            {
                colorIndex = i;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 26)
        {
            time -= Time.deltaTime;
            
        }
        if(Input.GetKeyDown(KeyCode.Tab) || time <= 0)
        {
            time = 0.6f;
            colorIndex = (colorIndex + palettes.Length) % palettes.Length;
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                audioSource.PlayOneShot(prev);

                for (int i = 0; i < textures.Length; i++)
                {
                    for (int j = 0; j < textures[i].height; j++)
                    {
                        for (int k = 0; k < textures[i].width; k++)
                        {
                            for (int l = 0; l < 4; l++)
                            {
                                if (textures[i].GetPixel(k, j) == palettes[(colorIndex + palettes.Length) % palettes.Length].colors[l])
                                {
                                    textures[i].SetPixel(k, j, palettes[(colorIndex - 1 + palettes.Length) % palettes.Length].colors[l]);
                                }

                            }
                        }
                    }
                    textures[i].Apply();
                }
                colorIndex--;
            }
            else
            {
                audioSource.PlayOneShot(next);
                for (int i = 0; i < textures.Length; i++)
                {
                    for (int j = 0; j < textures[i].height; j++)
                    {
                        for (int k = 0; k < textures[i].width; k++)
                        {
                            for (int l = 0; l < 4; l++)
                            {
                                if (textures[i].GetPixel(k, j) == palettes[(colorIndex + palettes.Length) % palettes.Length].colors[l])
                                {
                                    textures[i].SetPixel(k, j, palettes[(colorIndex + 1 + palettes.Length) % palettes.Length].colors[l]);
                                }

                            }
                        }
                    }
                    textures[i].Apply();
                }

                colorIndex++;
            }
        }
    }
}
