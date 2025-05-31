using System.Linq;
using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
//using static UnityEditor.Progress;

public class Input_Controller : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Transform hand;
    [SerializeField] private Transform handItemOldParent;
    private ItemPickUp handItem;
    private bool state_mov, state_cam;

    private bool stateCollision = false;
    [SerializeField] private GameObject ligh;
    [SerializeField] private Collider colBox;
    [SerializeField] private GameObject camPlayer;
    [SerializeField] private bool inventario = false;
    [SerializeField] private PhysicMaterial noFrictionMaterial; // Material con fricción 0
    [SerializeField] private PhysicMaterial defaultMaterial;   // Material original con fricción 0.6
    public bool Inventario { get => inventario; set => inventario = value; }

    public bool StateCam { get => state_cam; set => state_cam = value; }
    public bool IsMoving { get => state_mov; set => state_mov = value; }
    public bool StateCollision { get => stateCollision; }
    [SerializeField] private GameObject camAnchor;

    private void Update()
    {
        //CambiarObjetoManoConScroll();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SoltarObjetoMano();
        }
    }
    //private void CambiarObjetoManoConScroll()
    //{
    //    if (!InventoryManager.Instance) return;
    //    var items = InventoryManager.Instance.GetItems();
    //    if (items.Count == 0) return;

    //    float scroll = Input.GetAxis("Mouse ScrollWheel");
    //    if (Mathf.Abs(scroll) > 0.01f)
    //    {
    //        // Quitar el objeto actual de la mano si hay uno
    //        if (handItem != null)
    //        {
    //            Destroy(handItem.gameObject);
    //            handItem = null;
    //        }

    //        // Cambiar el índice según el scroll
    //        if (scroll > 0)
    //            selectedInventoryIndex = (selectedInventoryIndex + 1) % items.Count;
    //        else if (scroll < 0)
    //            selectedInventoryIndex = (selectedInventoryIndex - 1 + items.Count) % items.Count;

    //        // Obtener el InventoryItem y el Item
    //        var inventoryItem = items[selectedInventoryIndex];
    //        var item = inventoryItem.Item;

    //        // Eliminar el objeto del inventario (lista y UI)
    //        InventoryManager.Instance.DeleteFromInventory(inventoryItem);

    //        // Instanciar el objeto y ponerlo en la mano
    //        GameObject obj = Instantiate(item.ObjetoReferencia1, Vector3.zero, Quaternion.identity);
    //        ItemPickUp itemPickUp = obj.GetComponent<ItemPickUp>();
    //        PutItemInHand(itemPickUp);

    //        // Ajustar el índice si el inventario se vacía
    //        if (InventoryManager.Instance.GetItems().Count == 0)
    //            selectedInventoryIndex = 0;
    //        else if (selectedInventoryIndex >= InventoryManager.Instance.GetItems().Count)
    //            selectedInventoryIndex = 0;
    //    }
    //}
    private void SoltarObjetoMano()
    {
        if (handItem != null)
        {
            // Posición delante del jugador
            Vector3 dropPosition = transform.position + transform.forward * 1.0f + Vector3.up * 0.5f;

            // Instancia el objeto en el mundo
            GameObject obj = Instantiate(handItem.gameObject, dropPosition, Quaternion.identity);
            obj.GetComponent<Collider>().enabled = true;

            // Si el objeto tiene Rigidbody, lo activamos
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }
            Relocator relocator = obj.GetComponent<Relocator>();
            if (relocator)
            {
                relocator.SetAtFloor(dropPosition);
            }
            // Elimina el objeto de la mano
            Destroy(handItem.gameObject);

            handItem = null;
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
    public void InputInventario(CapsuleCollider playerCollider)
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
                playerCollider.material = noFrictionMaterial; // Cambia el material del jugador a uno sin fricción

            }
            else if (!inventario)
            {
                StateCam = false;
                IsMoving = false;
                Debug.Log("INVENTARIO CERRADO");
                gameManager.AbrirInventario(IsMoving);
                playerCollider.material = defaultMaterial; // Cambia el material del jugador a uno con fricción
            }
            if(camPlayer.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.velocity = Vector3.zero;
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
            //Debug.Log("Objeto detectado: " + hit.collider.gameObject.name + ", Tag: " + hit.collider.tag);

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
                        if (itemPickUp.Item.ItemGoInventory == ItemGoInventory.Yes)
                        {
                            itemPickUp.PickUp();
                            break;
                        }
                        else
                        {
                            PutItemInHand(itemPickUp);
                            break;
                        }
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
                        print($"el objeto place es {hit.transform.name} y su tag es {hit.transform.tag}");
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
                    if (hit.collider.CompareTag("SalaMaquinas")) 
                    {
                        Box_partsTIT box_PartsTIT = hit.collider.GetComponent<Box_partsTIT>();
                        if (box_PartsTIT != null)
                        {
                            // Obtén el ItemPickUp del Box_partsTIT
                            ItemPickUp iteme = box_PartsTIT.OnRaycastHit();

                            // Verifica si el item ya está en el inventario
                            if (InventoryManager.Instance.ComprobarItem(iteme.Item) == false)
                            {
                                // Si el item no estaba en el inventario, lo agrega y lo pone en la mano
                                PutItemInHand(iteme);
                            }
                            else
                            {
                                Debug.LogWarning($"El item {iteme.Item.itemName} ya está en el inventario y no se puede recoger nuevamente.");
                            }
                        }
                    }
                    if (hit.collider.name =="marco")
                    {
                        hit.collider.GetComponent<marco>().CodigoActivo();
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
            //print($"name item: {itemPickUp.Item.name},id: {itemPickUp.Item.Id}");
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
        print($"el objeto place es {place.name} y su tag es {place.tag}, el objeto que tengo en mano es{handItem.name} y su tag es {handItem.tag}");

        if (handItem.CompareTag("Engranaje"))
        {
            // Validar si la zona es válida para este engranaje
            if (EsZonaValidaParaEngranaje(handItem.gameObject, place))
            {
                handItem.transform.SetParent(handItemOldParent, true);
                handItem.transform.position = place.position;
                gameManager.DetectTruePosicionEngranaje();
                handItem = null;
                return;
            }
            else
            {
                // No es la zona correcta, vuelve a la mano
                handItem.transform.SetParent(hand, true);
                handItem.transform.localPosition = Vector3.zero;
                handItem.GetComponent<Collider>().enabled = false;
                print("no es la posicion correcta");
                return;
            }
            //handItem.transform.SetParent(handItemOldParent, true);
            //handItem.transform.position = place.position;
            //if (gameManager.DetectTruePosicionEngranaje())
            //{
            //    handItem.transform.SetParent(handItemOldParent, true);
            //    handItem.transform.position = place.position;

            //    return;
            //}
            //else
            //{
            //    handItem.transform.SetParent(hand, true);
            //    handItem.transform.localPosition = Vector3.zero;
            //    handItem.GetComponent<Collider>().enabled = false;
            //    print("no es la posicion correcta");
            //    return;
            //}
        }

        if (handItem.CompareTag("tuberia"))
        {
            print($"{handItem.name}");
            if (place.name == "ZoneTuberia")
            {
                print($"entras en el puzle del pasillo 1, este es el objeto place:{place}");
                Destroy(handItem.gameObject);
                place.GetChild(0).gameObject.SetActive(true);
                place.GetComponent<MeshRenderer>().enabled = false;
                gameManager.ComprobarTuberia();
                handItem = null;
                return;
            }
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
                handItem = null;
                return;
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
                handItem = null;
                return;
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
                handItem = null;
                return;
            }
            else if (place.name == "Zone_extintor")
            {
                print($"entras en el puzle de la sala de espera, este es el objeto place:{place}");
                Destroy(handItem.gameObject);
                place.GetChild(0).gameObject.SetActive(true);
                place.GetComponent<MeshRenderer>().enabled = false;
                gameManager.ComprobarPuzzleSalaEspera();
                handItem = null;
                return;
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
            handItem = null;
            return;
        }
        if (place.CompareTag("SalaMaquinas"))
        {
            if(place.name == "zone_tit_sup")
            {
                print("entras en sup");
                foreach(Transform child in place)
                {
                    ItemPickUp childpickup = child.GetComponent<ItemPickUp>();
                    if (handItem.Item.Id == childpickup.Item.Id)
                    {
                        Destroy(handItem.gameObject);
                        place.GetComponentInParent<CintaTitere>().titActivepart(child.gameObject, 1);
                        handItem = null;
                        break;
                    }
                }
            }
            else if(place.name == "zone_tit_inf")
            {
                print("entras en inf");
                foreach (Transform child in place)
                {
                    ItemPickUp childpickup = child.GetComponent<ItemPickUp>();
                    if (handItem.Item.Id == childpickup.Item.Id)
                    {
                        Destroy(handItem.gameObject);
                        place.GetComponentInParent<CintaTitere>().titActivepart(child.gameObject, 2);
                        handItem = null;
                        break;
                    }
                }
            }
        }
        if(place.CompareTag("llavePuerta"))
        {
            if(handItem.Item.Id == 18)
            {
                if (place.GetComponent<Puerta>().PuertaOpen(handItem))
                {
                    Destroy(handItem.gameObject);
                    handItem = null;
                    return;
                }
            }
        }
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
    private bool EsZonaValidaParaEngranaje(GameObject engranaje, Transform zona)
    {
        // Busca el índice del engranaje
        GameManager gm = gameManager;
        int idx = Array.IndexOf(gm.engranajes, engranaje);
        if (idx < 0) return false;

        // Busca el índice de la zona
        int zonaIdx = Array.IndexOf(gm.zonewin, zona.gameObject);
        if (zonaIdx < 0) return false;

        // Define las posiciones válidas
        int[][] validPositions = new int[][]
        {
        new int[] { 1, 3 },      // engranajes[0]
        new int[] { 0, 2, 4 },   // engranajes[1]
        new int[] { 0, 2, 4 },   // engranajes[2]
        new int[] { 1, 3 },      // engranajes[3]
        new int[] { 0, 2, 4 }    // engranajes[4]
        };

        return validPositions[idx].Contains(zonaIdx);
    }

    internal void InputSalir()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !inventario)
        {
            // Consulta el estado real del menú de pausa
            bool menuActivo = gameManager.PausaActiva;

            if (!menuActivo)
            {

                gameManager.salirJuego();
            }
            else
            {

                gameManager.returnGame();
            }
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
