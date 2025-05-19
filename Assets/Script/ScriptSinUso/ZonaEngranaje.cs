//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;



//public class ZonaEngranaje : MonoBehaviour /*, IDropHandler  // funciona con la funcion OnDrod  */
//{
//    public int idEngranajeCorrecto;
//    private bool engranajeColocado = false;
//    private ControladorJuego controladorJuego;

//    private void Start()
//    {
//        controladorJuego = FindObjectOfType<ControladorJuego>();
//    }
//    //private void OnTriggerEnter(Collider other)
//    //{

//    //}
//    //private void OnTriggerStay(Collider other)
//    //{

//    //}
//    //private void OnTriggerExit(Collider other)
//    //{

//    //}


//    public void OnDrop(PointerEventData eventData)
//    {
//        if (engranajeColocado) return;

//        var engranaje = eventData.pointerDrag;
//        if (engranaje != null && engranaje.GetComponent<Engranaje>().idEngranaje == idEngranajeCorrecto)
//        {
//            // Ajusta la posición del engranaje en la zona correcta y actualiza el estado
//            engranaje.transform.SetParent(transform);
//            engranaje.transform.position = transform.position;
//            engranajeColocado = true;

//            controladorJuego.EngranajeColocadoCorrectamente();
//        }
//    }


//}


