using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Collider colliderPuerta;
    private void Start()
    {
        anim.enabled = false;
    }
    public void puertaOpen()
    {
        anim.enabled = true;
        colliderPuerta.enabled = false;

    }
}
