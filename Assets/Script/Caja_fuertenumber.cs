using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caja_fuertenumber : MonoBehaviour
{
    [SerializeField] private Input_Controller _Controller;
    private float rotationX = 0f;
    private Renderer _renderer;
    private void Update()
    {
        if (_Controller.StateCollision && _Controller.Interact())
        {
        
            rotationX += 36f;

            if (rotationX > 360f)
            {
                rotationX = 0f;
            }
            print("rotationX: " + rotationX);

            
            transform.rotation = Quaternion.Euler(rotationX, 0.0000001f, 0.0000001f);
        }
    }
}