using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Maquina_escribir : MonoBehaviour
{
    [SerializeField] private GameObject paper;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Interact_object interact_Object;
    [SerializeField] private Input_Controller _Controller;
    [SerializeField] private string correctName = "abel";
    [SerializeField] GameManager gameManager;
    [SerializeField] public bool escribir;

    private string playerInput = "";

    private void Awake()
    {
        paper.SetActive(false);
        escribir = true;
    }

    private void Update()
    {
        if (interact_Object.CanInteract && _Controller.Interact_() && escribir)
        {
            print("entra a escribir");
            gameManager.HabilitarRaton(true); // Habilita el ratón para escribir.
            paper.SetActive(true);           // Muestra el papel.
            textMeshPro.text = "";           // Limpia el texto anterior.
            StartCoroutine(HandlePlayerInput());
        }
    }

    private IEnumerator HandlePlayerInput()
    {
        playerInput = "";
        while (paper.activeSelf)
        {
            foreach (char c in Input.inputString)
            {
                if (c == '\b' && playerInput.Length > 0)
                {
                    // Borrar el último carácter
                    playerInput = playerInput.Substring(0, playerInput.Length - 1);
                }
                else if (c == '\n' || c == '\r')
                {
                    // Al presionar Enter, verifica la entrada
                    if (playerInput.ToLower() == correctName.ToLower())
                    {
                        Debug.Log("¡Correcto! Puzzle resuelto.");
                        paper.SetActive(false);  // Oculta el papel
                        escribir = false;        // Desactiva la escritura
                        gameManager.HabilitarRaton(false); // Bloquea el ratón
                        gameManager.PuzleMaquinaEscribirCompletado();// Habilito la nota
                        yield break; // Termina la corrutina si se resuelve correctamente
                    }
                    else
                    {
                        Debug.Log("Incorrecto. Intenta de nuevo.");
                        playerInput = "";       // Reinicia el texto ingresado
                        textMeshPro.text = "";  // Limpia la pantalla
                    }
                }
                else if (playerInput.Length < 10)
                {
                    // Agregar el carácter ingresado
                    playerInput += c;
                }

                // Actualizar el texto en pantalla
                textMeshPro.text = playerInput;
            }

            yield return null;
        }
        #region intento1 de escribir
        //playerInput = "";
        //while (paper.activeSelf == true)
        //{
        //    foreach (char letra in Input.inputString)
        //    {
        //        if (letra == '\b' && playerInput.Length > 0)
        //        {
        //            playerInput = playerInput.Substring(0, playerInput.Length - 1);
        //        }
        //        else if (letra == '\n' || letra == '\r')
        //        {
        //            CheckInput();
        //            yield break;
        //        }
        //        else
        //        {
        //            playerInput += letra;
        //        }

        //        textMeshPro.text = playerInput;
        //    }

        //    yield return null;
        //}
        #endregion
    }
    // esto dependia del primer intento de escritura
    //private void CheckInput()
    //{
    //    if (playerInput.ToLower() == correctName.ToLower())
    //    {
    //        Debug.Log("¡Correcto! Puzzle resuelto.");
    //        paper.SetActive(false);  // Oculta el papel al resolver el puzzle.
    //        escribir = false;       // Desactiva la escritura.
    //        gameManager.HabilitarRaton(false); // Bloquea el ratón.
    //        gameManager.PuzleMaquinaEscribirCompletado();
    //    }
    //    else
    //    {
    //        Debug.Log("Incorrecto. Intenta de nuevo.");
    //        playerInput = "";       // Reinicia el texto ingresado.
    //        textMeshPro.text = "";  // Limpia la pantalla.

    //    }
    //}

    public void habilitandoDeshabilito()
    {
        if (!escribir) // Si el papel está oculto.
        {
            paper.SetActive(true); // Mostrar el papel.
            escribir = true;       // Habilitar la escritura.
            gameManager.HabilitarRaton(true); // Activar el ratón.
            Debug.Log("Habilitado para escribir.");
        }
        else // Si el papel está activo.
        {
            paper.SetActive(false); // Ocultar el papel.
            escribir = true;       // Desactivar la escritura.
            gameManager.HabilitarRaton(false); // Desactivar el ratón.
            Debug.Log("Deshabilitado para escribir.");
        }
    }
}
