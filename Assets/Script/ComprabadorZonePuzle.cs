using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComprabadorZonePuzle : MonoBehaviour
{
    [SerializeField] private Collider[] zonePuzle;
    [SerializeField] private GameObject[] puzle_object;
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

        }
    }
}
