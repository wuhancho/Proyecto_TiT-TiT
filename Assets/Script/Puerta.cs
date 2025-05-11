using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public bool PuertaOpen(ItemPickUp handItem)
    {
        print($"puerta {numeropuerta}");
        switch (numeropuerta)
        {
            case 1:
                anim.SetBool("puertaOpen", true);
                colliderPuerta.enabled = false;
                return true;
            case 2:
                if (handItem.Variant == "despacho1")
                {
                    anim.SetBool("puertadesp1", true);
                    colliderPuerta.enabled = false;
                    return true;
                }
                return false;
            case 3:
                if (handItem.Variant == "despacho2") { 
                    anim.SetBool("puertadesp2", true);
                colliderPuerta.enabled = false;
                return true;
                }
                return false;
            case 4:
                if (handItem.Variant == "salaSecreta")
                {
                    anim.SetBool("puertaOpen", true);
                    colliderPuerta.enabled = false;
                    return true;
                } return false;
            case 0:
                Debug.Log("No hay puerta");
                return false;
        }
        return false;
    }
}
