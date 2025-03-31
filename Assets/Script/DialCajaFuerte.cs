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
        Debug.Log($"Checking nameObj:{gameObject.name} Dial number: {(rotationX/36)%10}, Expected: {passwordNumber}, rotation:{rotationX}");
        transform.rotation = Quaternion.Euler(rotationX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        //transform.localRotation = Quaternion.Euler(rotationX, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
        //transform.RotateAround(transform.position, transform.right, rotationX);
        //Vector3 eulerAngles = new Vector3(rotationX, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
        //transform.Rotate(eulerAngles);
        //IsCorrectNumber();
    }

    public int SelectedNumber()
    {
        //print("rotationX: " + Mathf.RoundToInt(rotationX / 36f));
        Number = Mathf.RoundToInt(rotationX / 36f)%10;
        //Debug.Log($"Checking nameObj:{gameObject.name} Dial number: {Number}, Expected: {passwordNumber}");
        return Number;
    }

    public bool IsCorrectNumber()
    {
        //Debug.Log($"Checking nameObj:{gameObject.name} Dial number: {SelectedNumber()}, Expected: {passwordNumber}");
        return SelectedNumber() == passwordNumber;
    }

}
