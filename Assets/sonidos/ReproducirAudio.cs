//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ReproducirAudio : MonoBehaviour
//{

//    AudioSource fuenteAudio;

//    // Start is called before the first frame update
//    void Start()
//    {
//        fuenteAudio = GetComponent<AudioSource>();
//    }

//    // Update is called once per frame
//    void OnTriggerEnter()
//    {
//        gameObject.GetComponent<Collider>().enabled = false;
//        fuenteAudio.Play();
//    }
//    void OnTriggerExit()
//    {
//        gameObject.GetComponent<Collider>().enabled = true;

//    }
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReproducirAudio : MonoBehaviour
{
    AudioSource fuenteAudio;

    void Start()
    {
        fuenteAudio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!AudioManager.audioReproduciendose)
        {
            StartCoroutine(ReproducirUnaVez());
        }
    }

    IEnumerator ReproducirUnaVez()
    {
        AudioManager.audioReproduciendose = true;

        // Desactivar collider para evitar reentradas
        GetComponent<Collider>().enabled = false;

        fuenteAudio.Play();

        yield return new WaitForSeconds(fuenteAudio.clip.length);

        AudioManager.audioReproduciendose = false;

        // Reactivar el collider después si lo necesitas
        GetComponent<Collider>().enabled = true;
    }
}
