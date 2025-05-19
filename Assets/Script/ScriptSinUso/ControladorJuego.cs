//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//using UnityEngine.UI;

//public class ControladorJuego : MonoBehaviour
//{
//    public int totalEngranajes = 5;
//    private int engranajesCorrectos = 0;
//    public Text textoVictoria;

//    private void Start()
//    {
//        textoVictoria.gameObject.SetActive(false);
//    }

//    public void EngranajeColocadoCorrectamente()
//    {
//        engranajesCorrectos++;
//        if (engranajesCorrectos >= totalEngranajes)
//        {
//            MostrarVictoria();
//        }
//    }

//    private void MostrarVictoria()
//    {
//        textoVictoria.gameObject.SetActive(true);
//        textoVictoria.text = "¡Has colocado todos los engranajes correctamente!";
//    }
//}
