using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialCajaFuerte : MonoBehaviour
{
    [SerializeField] private int passwordNumber;
    private float rotationX;
    public void CambioNumero()
    {
        rotationX += 36f;

        if (rotationX > 360f)
        {
            rotationX = 0f;
        }
        print("rotationX: " + rotationX);


        transform.rotation = Quaternion.Euler(rotationX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    public int SelectedNumber()
    {
        return Mathf.RoundToInt(rotationX / 36f);
    }

    public bool IsCorrectNumber()
    {
        return SelectedNumber() == passwordNumber;
    }
}
