using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class marco : MonoBehaviour
{

    [SerializeField] private Input_Controller _Controller;
    [SerializeField] GameManager gameManager;
    [SerializeField] private GameObject codigo;
    private void Awake()
    {
       codigo.SetActive(false);

    }
    public void MostrarCodigo()
    {
        if (codigo.activeSelf)
        {
            codigo.SetActive(false);
            gameManager.HabilitarRaton(false);
        }
        else
        {
            codigo.SetActive(true);
            gameManager.HabilitarRaton(true);
        }
    }
    public void CodigoActivo()
    {
        gameManager.HabilitarRaton(true);
        codigo.SetActive(true);
    }
}