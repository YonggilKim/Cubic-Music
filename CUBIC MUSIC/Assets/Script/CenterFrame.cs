using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CenterFrame : MonoBehaviour
{
    AudioSource myAudio;
    bool musicStart = false;
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision enter");

        if (musicStart == false)
        {
            if (collision.CompareTag("Note"))
            {
                Debug.Log("music start");
                myAudio.Play();
                musicStart = true;
            }
        }

    }

}
