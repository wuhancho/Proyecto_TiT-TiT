using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DialCajaFuerte : MonoBehaviour
{
    #region
    [SerializeField] private int passwordNumber;  // Número correcto para el dial
    private float rotationX;  // Usamos Z en lugar de X
    private int Number;
    private bool isRotating = false;  // Para evitar múltiples clics mientras rota

    public void CambioNumero()
    {
        if (!isRotating)
        {
            float newRotation = NormalizeAngle(rotationX + 36f);  // Incrementa el ángulo
            StartCoroutine(RotateDialSmoothly(newRotation));  // Rota de manera suave
        }
    }

    private IEnumerator RotateDialSmoothly(float targetRotation)
    {
        isRotating = true;
        //float targetRotation = NormalizeAngle(rotationX + angle); // Calcula el nuevo ángulo
        float startRotation = rotationX;
        float elapsedTime = 0f;
        float duration = 0.3f; // Tiempo de la animación

        if (targetRotation < startRotation && Mathf.Abs(targetRotation - startRotation) > 180)
        {
            targetRotation += 360;
        }
        else if (targetRotation > startRotation && Mathf.Abs(targetRotation - startRotation) > 180)
        {
            targetRotation -= 360;
        }
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            rotationX = Mathf.Lerp(startRotation, targetRotation, elapsedTime / duration);
            transform.localRotation = Quaternion.Euler(rotationX, transform.eulerAngles.y*0, transform.eulerAngles.z * 0);
            yield return null;
        }

        rotationX = targetRotation;
        transform.localRotation = Quaternion.Euler(rotationX, transform.eulerAngles.y * 0, transform.eulerAngles.z * 0);
        isRotating = false;

        Debug.Log($"Checking nameObj:{gameObject.name} Dial number: {SelectedNumber()}, Expected: {passwordNumber}, Rotation: {rotationX}");

        IsCorrectNumber();
    }

    public int SelectedNumber()
    {
        Number = Mathf.RoundToInt(rotationX / 36f) % 10;
        return Number;
    }

    public bool IsCorrectNumber()
    {
        bool correct = SelectedNumber() == passwordNumber;
        if (correct)
        {
            Debug.Log($"¡El dial {gameObject.name} está en la posición correcta!");
        }
        return correct;
    }

    private float NormalizeAngle(float angle)
    {
        while (angle < 0) angle += 360f;
        while (angle >= 360f) angle -= 360f;
        return angle;
    }
    #endregion
    #region primer intento
    //[SerializeField] private int passwordNumber;
    //private float rotationX;
    //private int Number;
    //public void CambioNumero()
    //{
    //    rotationX += 36f;
    //    if (transform.rotation.x >= 360f)
    //    {
    //        rotationX = 0f;
    //    }
    //    rotationX = NormalizeAngle(rotationX);
    //    //print("rotationX: " + rotationX);
    //    Debug.Log($"Checking nameObj:{gameObject.name} Dial number: {(rotationX/36)%10}, Expected: {passwordNumber}, rotation:{rotationX}");
    //    transform.rotation = Quaternion.Euler(rotationX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

    //    //transform.localEulerAngles = new Vector3(rotationX, transform.localEulerAngles.y, transform.localEulerAngles.z);
    //    //Vector3 rotatio = new Vector3(rotationX, 0, 0);
    //    //transform.Rotate(rotatio);

    //    //transform.localRotation = Quaternion.Euler(rotationX, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);

    //    //transform.RotateAround(transform.position, transform.right, rotationX);

    //    //Vector3 eulerAngles = new Vector3(rotationX, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
    //    //transform.Rotate(eulerAngles);

    //    //IsCorrectNumber();
    //}

    //public int SelectedNumber()
    //{
    //    //print("rotationX: " + Mathf.RoundToInt(rotationX / 36f));
    //    Number = Mathf.RoundToInt(rotationX / 36f)%10;
    //    //Debug.Log($"Checking nameObj:{gameObject.name} Dial number: {Number}, Expected: {passwordNumber}");
    //    return Number;
    //}

    //public bool IsCorrectNumber()
    //{
    //    //Debug.Log($"Checking nameObj:{gameObject.name} Dial number: {SelectedNumber()}, Expected: {passwordNumber}");
    //    return SelectedNumber() == passwordNumber;
    //}
    //private float NormalizeAngle(float angle)
    //{
    //    while (angle < 0) angle += 360f;
    //    while (angle >= 360f) angle -= 360f;
    //    return angle;
    //}
    #endregion
}
