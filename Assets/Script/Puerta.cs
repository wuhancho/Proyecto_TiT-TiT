using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Collider colliderPuerta;
    [SerializeField] int numeropuerta;
    private void Start()
    {
        anim.enabled = true;
    }
    public void puertaOpen()
    {
        switch (numeropuerta)
        {
            case 1:
                anim.SetBool("puertaOpen", true);
                break;
            case 2:
                anim.SetBool("puertadesp1", true);
                break;
            case 3:
                anim.SetBool("puertadesp2", true);
                break;
            case 4:
                anim.SetBool("puertaOpen", true);
                break;
        }
        colliderPuerta.enabled = false;
    }
}
