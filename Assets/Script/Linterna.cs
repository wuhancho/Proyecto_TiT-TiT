using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linterna : MonoBehaviour
{
    private Material[] materials;
    private Color color;
    private void OnTriggerEnter(Collider other)
    {
        print("entra en el trigger de la linterna");

        if (other.CompareTag("ViewLint"))
        {
            //meshRender = other.GetComponent<MeshRenderer>();
            print("entra en el if de la linterna");
            if (other.GetComponent<MeshRenderer>())
            {
                materials = other.GetComponent<MeshRenderer>().materials;
                if (materials[1].name == "base 2 (Instance)")
                {
                    print("entra en el if de la linterna 2");
                    color = materials[1].color;

                }
            }
            else if (other.gameObject.name == "L_")
            {
                
            }
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
            materials = other.GetComponent<MeshRenderer>().materials;
            color = materials[1].color;
            color.a = 0;
            materials[1].color = color;
        }
    }
}
