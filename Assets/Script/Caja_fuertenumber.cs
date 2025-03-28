using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Caja_fuertenumber : MonoBehaviour
{
    [SerializeField] private Input_Controller _Controller;
    private float rotationX;
    [SerializeField] private GameObject dial1,dial2,dial3,dial4;

    private void CambioNumero(GameObject dial)
    {        
            rotationX += 36f;

            if (rotationX > 360f)
            {
                rotationX = 0f;
            }
            print("rotationX: " + rotationX);


            dial.transform.rotation = Quaternion.Euler(rotationX, dial.transform.rotation.eulerAngles.y, dial.transform.rotation.eulerAngles.z);
    }
}