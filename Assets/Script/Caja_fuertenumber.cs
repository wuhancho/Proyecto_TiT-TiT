using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Caja_fuertenumber : MonoBehaviour
{
    [SerializeField] private Input_Controller _Controller;
    private float rotationX;
    //[SerializeField] private GameObject dial1,dial2,dial3,dial4;
    private DialCajaFuerte[] diales;
 
    private void Awake()
    {
        diales = GetComponentsInChildren<DialCajaFuerte>();
    }

    public bool IsCorrectPassword()
    {
        for (int i = 0; i < diales.Length; i++)
        {
            if (!diales[i].IsCorrectNumber())
            {
                return false;
            }
        }
        return true;
    }
    private void Update()
    {
        if(!IsCorrectPassword())
        {
            Debug.Log("Contraseña correcta");
        }
    }

    //public void CualDial(GameObject dial)
    //{
    //    if (dial == dial1)
    //    {
    //        CambioNumero(dial1);
    //    }
    //    else if (dial == dial2)
    //    {
    //        CambioNumero(dial2);
    //    }
    //    else if (dial == dial3)
    //    {
    //        CambioNumero(dial3);
    //    }
    //    else if (dial == dial4)
    //    {
    //        CambioNumero(dial4);
    //    }
    //    else
    //    {
    //        Debug.Log("No se ha seleccionado ningun dial");
    //    }
    //}

    //private void CambioNumero(GameObject dial)
    //{        
    //        rotationX += 36f;

    //        if (rotationX > 360f)
    //        {
    //            rotationX = 0f;
    //        }
    //        print("rotationX: " + rotationX);


    //        dial.transform.rotation = Quaternion.Euler(rotationX, dial.transform.rotation.eulerAngles.y, dial.transform.rotation.eulerAngles.z);
    //}

}