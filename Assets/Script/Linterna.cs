using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linterna : MonoBehaviour
{
    [SerializeField] Material Transparent;
    [SerializeField] MeshRenderer meshRender;
    private void OnTriggerEnter(Collider other)
    {
        print("entra en el trigger de la linterna");
        if (other.CompareTag("ViewLint"))
        {
            //meshRender = other.GetComponent<MeshRenderer>();
            print("entra en el if de la linterna");
            var materials = other.GetComponent<MeshRenderer>().materials;
            var color = materials[1].color;
            color.a = 1f;
            materials[1].color = color;
            //if (other.GetComponent<MeshRenderer>().materials[1].name=="base 2"/*meshRender.materials[1].name=="base 2" || meshRender.materials[0].name == "base 2"*/)
            //{
            //    print("entra en el if de la linterna 2");
            //}
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ViewLint"))
        {
            var materials = other.GetComponent<MeshRenderer>().materials;
            var color = materials[1].color;
            color.a = 0;
            materials[1].color = color;
        }
    }
}
