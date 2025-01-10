using UnityEngine;
//using static UnityEditor.Progress;

public class Input_Controller : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Transform hand;
    private Transform handItemOldParent;
    private ItemPickUp handItem;
    private bool state = false;

    public bool State { get => state; set => state = value; }

    public Vector3 MoveInput()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Jump");
        float z = Input.GetAxis("Vertical");
        return new Vector3(x, y, z);
    }
    public Vector2 MouseInput()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        return new Vector2(x, y);
    }
    public bool Interact()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("entra La mouse0");
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool Interact_()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log("entra La mouse0");
            return true;
        }
        else
        {
            return false;
        }
    }
    public void InputInventario()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            state = !state; // Cambia el estado
            //Debug.Log(state);
            if (state == true)
            {
                Debug.Log("INVENTARIO ABIERTO");
                gameManager.AbrirInventario(state);
            }
            else if (state == false)
            {
                Debug.Log("INVENTARIO CERRADO");
                gameManager.AbrirInventario(state);
            }
        }

    }

    public void RayCoger(float distance)
    {

        Vector3 origin = Camera.main.transform.position;
        Vector3 direction = Camera.main.transform.forward;
        RaycastHit hit; 
        if (Physics.Raycast(origin, direction, out hit, distance))
        {
            Debug.Log("Objeto detectado: " + hit.collider.gameObject.name + ", Tag: " + hit.collider.tag);

            Debug.DrawRay(origin, direction * distance, Color.red, 3);

            // Debug.DrawRay(origin, direction*distance, Color.red);
            //if (hit.collider != null && hit.collider.CompareTag("Engranaje"))
            //{
            //    Debug.DrawRay(origin, direction, Color.green, 3);
            //    MoverEngranajeConCursor();
            //}

            ItemPickUp itemPickUp = hit.collider.gameObject.GetComponent<ItemPickUp>();
            if (itemPickUp != null)
            {
                switch (itemPickUp.Type)
                {
                    case ItemType.PickableObject:
                        itemPickUp.PickUp();
                        break;
                    case ItemType.HandItem:
                        PutItemInHand(itemPickUp);
                        break;
                }
            }
            else
            {
                if (handItem)
                {
                    if (hit.transform.CompareTag(handItem.HandItemPlaceTagName))
                    {
                        PutHandItemInPlace(hit.transform);
                    }
                }
            }
        }
    }

    public bool PutItemInHand(ItemPickUp itemPickUp)
    {
        if (handItem == null)
        {
            handItem = itemPickUp;
            handItemOldParent = itemPickUp.transform.parent;
            itemPickUp.transform.SetParent(hand, true);
            itemPickUp.transform.localPosition = Vector3.zero;
            return true;
        }
        return false;
    }

    private void PutHandItemInPlace(Transform place)
    {
        if (handItem.name != "chincheta")
        {
            handItem.transform.SetParent(handItemOldParent, true);
            handItem.transform.position = place.position;
            if (handItem.CompareTag("Engranaje"))
            {
                gameManager.DetectTruePosicionEngranaje();
            }
        }

        if (handItem.name == "chincheta")
        {// pone la chincheta en su localizacion.
            handItem.transform.SetParent(handItemOldParent, true);
            handItem.transform.position = place.position;
            if (handItemOldParent.name == "pais")
            {
                gameManager.PuzleMapaCompletado();
                handItem.GetComponent<Collider>().enabled = false;
            }
            //Vector3 HandPutInObj = new Vector3(place.position.x, place.position.y, place.position.z);
            //handItem.transform.position = HandPutInObj;
            //handItem.transform.rotation = place.rotation;
            //handItem.transform.localScale = place.localScale;
        }
        if (handItem.CompareTag("PuzleSalaEspera"))
        {
            print($"entras en el puzle de la sala de espera, este es el objeto place:{place}"); 
            handItem.transform.SetParent(place, true);
            handItem.transform.position = place.position;
            
        }
        handItem = null;
    }
    public void InteractItemHand(ItemPickUp item)
    {
        //if (handItem == null)
        //{
        //    PutItemInHand(item);
        //    Debug.Log("El objeto fue colocado en la mano.");
        //}
        //else
        //{
        //    Debug.LogWarning("Ya tienes un objeto en la mano. Primero suéltalo.");
        //}
        if (handItem == null)
        {
            PutHandItemInPlace(item.transform);
        }
    }


    //public void RayCoger(float distance)
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Vector3 origin = Camera.main.transform.position;
    //        Vector3 direction = Camera.main.transform.forward;
    //        RaycastHit hit;
    //        if (Physics.Raycast(origin, direction, out hit, distance))
    //        {

    //        }
    //    }

    //}

    // parte de ignacio
    //public void InputInventario()
    //{
    //    if (Input.GetKeyDown(KeyCode.Tab))
    //    {
    //        gameManager.AbrirInventario();
    //    }
    //}
}
