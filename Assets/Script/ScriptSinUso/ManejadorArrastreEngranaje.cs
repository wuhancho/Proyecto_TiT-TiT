//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.EventSystems;


//public class ManejadorArrastreEngranaje : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
//{
//    private Vector3 posicionInicial;
//    private Transform padreOriginal;
//    private CanvasGroup grupoCanvas;
//    private GameObject objetoArrastrado; // Referencia al engranaje que estamos moviendo
//    private Vector3 offset;

//    private void Awake()
//    {
//        grupoCanvas = GetComponent<CanvasGroup>();
//    }

//    public void OnBeginDrag(PointerEventData eventData)
//    {

//        posicionInicial = transform.position;
//        padreOriginal = transform.parent;
//        //grupoCanvas.blocksRaycasts = false; // Desactivar los raycasts para evitar conflictos al soltar
//    }

//    public void OnDrag(PointerEventData eventData)
//    {
//        transform.position = Input.mousePosition; // Mueve el engranaje con el cursor
//    }

//    public void OnEndDrag(PointerEventData eventData)
//    {
//        Debug.Log("entra");
//        grupoCanvas.blocksRaycasts = true;

//        // Si el engranaje no fue colocado en una zona correcta, vuelve a su posición inicial
//        if (transform.parent == padreOriginal)
//        {
//            transform.position = posicionInicial;
//        }
//    }

//    private void Update()
//    {
//        // Detectar cuando se hace clic izquierdo
//        if (Input.GetMouseButtonDown(0))
//        {
//            DetectarYSeleccionarEngranaje();
//        }

//        // Si estamos arrastrando un engranaje, moverlo con el cursor
//        if (objetoArrastrado != null)
//        {
//            MoverEngranajeConCursor();
//        }

//        // Soltar el engranaje cuando se suelta el clic
//        if (Input.GetMouseButtonUp(0))
//        {
//            objetoArrastrado = null;
//        }
//    }

//    // Detecta si el clic inicial fue sobre un engranaje
//    private void DetectarYSeleccionarEngranaje()
//    {
//        // Convertir la posición del ratón en coordenadas del mundo
//        Vector3 posicionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//        Vector2 posicionMouse2D = new (posicionMouse.x, posicionMouse.y);

//        // Hacer un raycast para detectar el objeto bajo el cursor
//        RaycastHit2D hit = Physics2D.Raycast(posicionMouse2D, Vector2.zero);

//        if (hit.collider != null && hit.collider.CompareTag("Engranaje"))
//        {
//            // Si se hace clic en un objeto con el tag "Engranaje", seleccionarlo
//            objetoArrastrado = hit.collider.gameObject;

//            // Calcular el offset para mover el engranaje sin que salte
//            offset = objetoArrastrado.transform.position - posicionMouse;
//        }
//    }

//    // Mueve el engranaje seleccionado con el cursor
//    private void MoverEngranajeConCursor()
//    {
//        // Actualizar la posición del engranaje para que siga al cursor
//        Vector3 posicionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//        objetoArrastrado.transform.position = posicionMouse + offset;
//    }
//    //Engranaje[] todosLosEngranajes = FindAnyObjectByType<Engranaje>();


//    //foreach (Engranaje engranaje in todosLosEngranajes)
//    //{
//    //    Debug.Log("ID del engranaje:" + engranaje.idEngranaje);
//    //}
//}


