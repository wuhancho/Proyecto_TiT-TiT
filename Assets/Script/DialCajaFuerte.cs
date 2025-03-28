using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialCajaFuerte : MonoBehaviour
{
    [SerializeField] private int passwordNumber;
    private float rotationX;
    private int Number;
    public void CambioNumero()
    {
        rotationX += 36f;

        if (rotationX >= 360f)
        {
            rotationX = 0f;
        }
        print("rotationX: " + rotationX);

        transform.rotation = Quaternion.Euler(rotationX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    public int SelectedNumber()
    {
        print("rotationX: " + Mathf.RoundToInt(rotationX / 36f));
        switch (Mathf.RoundToInt(rotationX / 36f))
        {
            case 0:
            case 360:
                Number = 0;
                break;
            case 36:
                Number = 1;
                break;
            case 72:
                Number = 2;
                break;
            case 108:
                Number = 3;
                break;
            case 144:
                Number = 4;
                break;
            case 180:
                Number = 5;
                break;
            case 216:
                Number = 6;
                break;
            case 252:
                Number = 7;
                break;
            case 288:
                Number = 8;
                break;
            case 324:
                Number = 9;
                break;
            default:
                Number = -1; // Valor por defecto en caso de error
                break;
        }
        print("Number: " + Number);
        return Number;
    }

    public bool IsCorrectNumber()
    {
        return SelectedNumber() == passwordNumber;
    }
}
