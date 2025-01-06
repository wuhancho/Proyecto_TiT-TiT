using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public enum ItemType { None, PickableObject, HandItem }
public class ItemPickUp : MonoBehaviour
{
    [SerializeField] private ItemType typeObject;
    [SerializeField] private Item item;
    //private float distan = 1.5f;
    //[SerializeField] private GameObject personaje;
    [SerializeField] private string handItemPlaceTagName;

    public ItemType Type { get => typeObject; }
    public string HandItemPlaceTagName { get => handItemPlaceTagName; }

    public void PickUp()
    {
       // print($"se añade el objeto {item.ObjetoReferencia1.name}");
        bool pickedUp = InventoryManager.Instance.Add(item);
        if (pickedUp)
        {
            Destroy(gameObject);
        }
    }
    //private void OnMouseDown()
    //{
    //    //if (item != null)
    //    //{
    //    //    PickUp();
    //    //}
    //    Vector3 origin = Camera.main.transform.position;
    //    Vector3 direction = Camera.main.transform.forward;
    //    RaycastHit hit;
    //    Debug.Log("realiza el mouse");
    //    if (Physics.Raycast(origin, direction, out hit, distan))
    //    {
    //        Debug.Log("Objeto detectado: " + hit.collider.gameObject.name + ", Tag: " + hit.collider.tag);

    //        Debug.DrawRay(origin, direction * distan, Color.red, 3);

    //        // Debug.DrawRay(origin, direction*distance, Color.red);
    //        if (hit.collider != null && hit.collider.CompareTag("Engranaje"))
    //        {
    //            Debug.DrawRay(origin, direction, Color.green, 3);
    //        }
    //        if (item != null)
    //        {
    //            PickUp();
    //        }
    //    }
    //}
    #region intento de Raycast
    //private void Update()
    //{
    //    RayCoger(distan);
    //}
    //public void RayCoger(float distance)
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Vector3 origin = Camera.main.transform.position;
    //        Vector3 direction = Camera.main.transform.forward;
    //        RaycastHit hit;
    //        if (Physics.Raycast(origin, direction, out hit, distance))
    //        {
    //            Debug.Log("Objeto detectado: " + hit.collider.gameObject.name + ", Tag: " + hit.collider.tag);

    //            Debug.DrawRay(origin, direction * distance, Color.red, 3);

    //            // Debug.DrawRay(origin, direction*distance, Color.red);
    //            if (hit.collider != null && hit.collider.CompareTag("Engranaje"))
    //            {
    //                Debug.DrawRay(origin, direction, Color.green, 3);
    //                MoverEngranajeConCursor();
    //            }
    //            if (item != null)
    //            {
    //                PickUp();
    //            }
    //        }
    //    }
    //}
    //private void MoverEngranajeConCursor()
    //{
    //    // Actualizar la posición del engranaje para que siga al cursor
    //    Vector3 posicionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    gameObject.transform.position = posicionMouse;
    //}
    #endregion
}

