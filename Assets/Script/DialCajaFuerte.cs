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
        //print("rotationX: " + rotationX);

        transform.rotation = Quaternion.Euler(rotationX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        //IsCorrectNumber();
    }

    public int SelectedNumber()
    {
        //print("rotationX: " + Mathf.RoundToInt(rotationX / 36f));
        Number = Mathf.RoundToInt(rotationX / 36f)%10;
        return Number;
    }

    public bool IsCorrectNumber()
    {
        //Debug.Log($"Checking Dial: {SelectedNumber()}, Expected: {passwordNumber}");
        return SelectedNumber() == passwordNumber;
    }

}
