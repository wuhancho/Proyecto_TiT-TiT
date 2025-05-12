using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PararAudio : MonoBehaviour
{

    AudioSource fuente1Audio;

    // Start is called before the first frame update
    void Start()
    {
        fuente1Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void OnTriggerEnter    ()
    {
        fuente1Audio.Stop();
    }
}
