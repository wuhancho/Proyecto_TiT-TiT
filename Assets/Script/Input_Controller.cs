using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
//using static UnityEditor.Progress;

public class Input_Controller : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Transform hand;
    private Transform handItemOldParent;
    private ItemPickUp handItem;
    private bool state_mov, state_cam;

    private bool stateCollision = false;
    [SerializeField] private GameObject ligh;
    [SerializeField] private Collider colBox;
    [SerializeField] private GameObject camPlayer;
    [SerializeField] private bool inventario = false;
    public bool Inventario { get => inventario; set => inventario = value; }

    public bool StateCam { get => state_cam; set => state_cam = value; }
    public bool IsMoving { get => state_mov; set => state_mov = value; }
    public bool StateCollision { get => stateCollision; }
    [SerializeField] private GameObject camAnchor;
    private void Update()
    {
        //state_cam = StateCam;
        //state_mov = IsMoving;
        if (Input.GetKeyDown(KeyCode.N))
        {
            StartCoroutine(gameManager.MostrarEstado(StateCam, IsMoving));
        }
    }
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
    public bool Interact()// para interactuar general
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && stateCollision)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Interact_() // para que el jugador deje los objetos de la mano al inventario
    {
        if (handItem != null)
        {
            handItem.PickUp();
        }
    }
    public void InputInventario()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {

            inventario = !inventario;
            if (camPlayer.transform.parent != camAnchor)
            {
                ReturnCameraToPlayer(camPlayer);
            }
            if (inventario)
            {
                StateCam = true;
                IsMoving = true;
                Debug.Log("INVENTARIO ABIERTO");
                gameManager.AbrirInventario(IsMoving);
            }
            else if (!inventario)
            {
                StateCam = false;
                IsMoving = false;
                Debug.Log("INVENTARIO CERRADO");
                gameManager.AbrirInventario(IsMoving);
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
            else if (handItem)
            {
                if (handItem)
                {
                    if (hit.transform.CompareTag(handItem.HandItemPlaceTagName))
                    {
                        //print("entra en el if de poner el objeto en su lugar");
                        PutHandItemInPlace(hit.transform);
                    }
                }
            }
            else
            {
                if (hit.collider != null)
                {
                    stateCollision = true;
                    if (hit.collider.CompareTag("Caja_Fuerte"))
                    {
                        if(hit.collider.GetComponent<Caja_fuertenumber>() != null)
                        {
                            Caja_fuertenumber caja_fuertenumber = hit.collider.GetComponent<Caja_fuertenumber>();
                            caja_fuertenumber.changeCamPivot(camPlayer);
                        }
                        //Caja_fuertenumber caja_fuertenumber = hit.collider.GetComponent<Caja_fuertenumber>();
                        DialCajaFuerte dial = hit.collider.GetComponent<DialCajaFuerte>();

                        if (dial != null)
                        {
                            dial.CambioNumero();
                        }
                        //else
                        //{
                        //    print(" y mis números?");
                        //}
                    }
                }
                else
                {
                    stateCollision = false;
                }
            }
        }
    }
    public void ReturnCameraToPlayer(GameObject CamPlayerObj)
    {
        CamPlayerObj.transform.SetParent(camAnchor.transform);
        CamPlayerObj.transform.localPosition = Vector3.zero;
        CamPlayerObj.transform.localRotation = Quaternion.identity;
        IsMoving = false;
        StateCam = false;
    }
    public bool PutItemInHand(ItemPickUp itemPickUp)
    {
        if (handItem == null)
        {
            handItem = itemPickUp;
            handItemOldParent = itemPickUp.transform.parent;
            itemPickUp.transform.SetParent(hand, true);
            itemPickUp.transform.localPosition = Vector3.zero;
            itemPickUp.GetComponent<Collider>().enabled = false;
            activeLight();
            return true;
        }
        return false;
    }

    private void activeLight()
    {
        if (handItem.CompareTag("linterna"))
        {
            ligh.SetActive(true);
        }
        else
        {
            ligh.SetActive(false);
        }
    }
    private void PutHandItemInPlace(Transform place)
    {
        if (handItem.CompareTag("Engranaje"))
        {
            handItem.transform.SetParent(handItemOldParent, true);
            handItem.transform.position = place.position;
            gameManager.DetectTruePosicionEngranaje();

        }


        if (handItem.name == "Chincheta" || handItem.name == "Chincheta(Clone)")
        {// pone la chincheta en su localizacion.
            print("nombre del objeto colicion" + place.name);
            //print("entro en el if de la chincheta");
            //print("este es el objeto anterior"+handItemOldParent.name);
            if (place.name == "Pais_Chincheta")
            {
                print("entro en el if de la chincheta en el pais");
                //gameManager.PuzleMapaCompletado();
                handItem.transform.SetParent(place.parent, true);
                //handItem.transform.position = place.position;
                handItem.GetComponent<Collider>().enabled = false;
                handItem.transform.localPosition = place.localPosition;
                place.gameObject.SetActive(false);
                gameManager.PuzleMapaCompletado();
            }
            //Vector3 HandPutInObj = new Vector3(place.position.x, place.position.y, place.position.z);
            //handItem.transform.position = HandPutInObj;
            //handItem.transform.rotation = place.rotation;
            //handItem.transform.localScale = place.localScale;
        }
        if (handItem.CompareTag("PuzleSalaEspera"))
        {
            if (place.name == "Zone_cuadro_grande")
            {
                print($"entras en el puzle de la sala de espera, este es el objeto place:{place}");
                Destroy(handItem.gameObject);
                place.GetChild(0).gameObject.SetActive(true);
                place.GetComponent<MeshRenderer>().enabled = false;
                gameManager.ComprobarPuzzleSalaEspera();
            }
            else if (place.name == "Zone_radio")
            {
                print($"entras en el puzle de la sala de espera, este es el objeto place:{place}");
                //handItem.transform.SetParent(place, true);
                //handItem.transform.position = place.position;
                Destroy(handItem.gameObject);
                place.GetChild(0).gameObject.SetActive(true);
                place.GetComponent<MeshRenderer>().enabled = false;
                gameManager.ComprobarPuzzleSalaEspera();
            }
            else if (place.name == "Zone_extintor")
            {
                print($"entras en el puzle de la sala de espera, este es el objeto place:{place}");
                Destroy(handItem.gameObject);
                place.GetChild(0).gameObject.SetActive(true);
                place.GetComponent<MeshRenderer>().enabled = false;
                gameManager.ComprobarPuzzleSalaEspera();
            }

        }
        if (place.CompareTag("caja_puzzle"))
        {
            print($"entras en el puzle de la sala de espera, este es el objeto place:{place}");
            Destroy(handItem.gameObject);
            place.GetChild(0).gameObject.SetActive(true);
            place.GetComponent<MeshRenderer>().enabled = false;
            place.GetComponent<Collider>().enabled = false;
            place.GetComponent<BoxCollider>().isTrigger = false;
            colBox.enabled = true;
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
