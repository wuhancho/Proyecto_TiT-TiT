using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chincheta : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    Transform parentChincheta;
    //Collider chinchetaCollider;
    private void Update()
    {
        parentChincheta = transform.parent;
        if (parentChincheta.name == "Pais_Chincheta")
        {
            //chinchetaCollider.enabled = false;
            gameManager.PuzleMapaCompletado();
            //gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}
