using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linterna : MonoBehaviour
{
    private Dictionary<Collider, bool> activeColliders = new Dictionary<Collider, bool>();
    private Material[] materials;
    private Color color;
    [SerializeField] private float maxAlpha = 1f; // Máximo alpha permitido
    private int collidersInside = 0; // Contador de triggers activos
    //[SerializeField] private GameObject litgh;
    //[SerializeField] private Vector3 PositionPlayerHigh;
    //private void Start()
    //{
    //    litgh.SetActive(false);
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ViewLint") && !activeColliders.ContainsKey(other))
        {
            activeColliders[other] = true; // Se registra que este collider ha entrado
            collidersInside++;
            UpdateAlpha(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ViewLint") && activeColliders.ContainsKey(other))
        {
            activeColliders.Remove(other); // Se elimina el collider del diccionario
            collidersInside = Mathf.Max(0, collidersInside - 1); // Evita valores negativos
            UpdateAlpha(other);
        }
    }

    private void UpdateAlpha(Collider other)
    {
        if (other.transform.parent != null) // Se obtiene el padre
        {
            MeshRenderer meshRenderer = other.transform.parent.GetComponent<MeshRenderer>();
            if (meshRenderer)
            {
                materials = meshRenderer.materials;
                if (materials.Length > 1) // Asegura que el índice 1 exista
                {
                    color = materials[1].color;
                    color.a = Mathf.Clamp((float)collidersInside / 5f * maxAlpha, 0, maxAlpha); // Alpha proporcional
                    materials[1].color = color;
                }
            }
        }
    }
    //private void Update()
    //{

    //    if (gameObject.transform.parent.name =="Hand")
    //    {
    //        litgh.SetActive(true);
    //    }
    //    else
    //    {
    //         litgh.SetActive(false);
    //    }
    //}
    //private Material[] materials;
    //private Color color;
    //[SerializeField] private float alpha;
    //private void OnTriggerEnter(Collider other)
    //{
    //    print("entra en el trigger de la linterna");

    //    if (other.CompareTag("ViewLint"))
    //    {
    //        //meshRender = other.GetComponent<MeshRenderer>();
    //        print("entra en el if de la linterna");
    //        if (other.GetComponent<MeshRenderer>())
    //        {
    //            materials = other.GetComponent<MeshRenderer>().materials;
    //            if (materials[1].name == "base 2 (Instance)")
    //            {
    //                print("entra en el if de la linterna 2");
    //                color = materials[1].color;
    //                color.a = alpha;
    //                materials[1].color = color;
    //            }
    //        }
    //        else if (other.gameObject.name == "L_")
    //        {

    //        }
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("ViewLint"))
    //    {
    //        materials = other.GetComponent<MeshRenderer>().materials;
    //        color = materials[1].color;
    //        color.a = 0;
    //        materials[1].color = color;
    //    }
    //}
}
