using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


    }

    // Update is called once per frame
    public void salir()
    {
        Debug.Log("Saliendo del juego");
        Application.Quit();


    }
}
