using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    private List<InventoryItem> items = new List<InventoryItem>();
    public RectTransform inventoryContent;
    public InventoryItem inventoryItemPrefab;
    public Toggle EnableRemove;
    public Vector2Int gridSize = new Vector2Int(4, 4);
    public Vector2 slotSize = new Vector2(100, 100);
    private InventoryItem[,] grid;
    //[SerializeField] private Transform hand;
    [SerializeField] private GameObject player;
    private Input_Controller inputController;


    private void Awake()
    {
        Instance = this;
        grid = new InventoryItem[gridSize.x, gridSize.y];
        inputController = player.GetComponent<Input_Controller>();
    }

    private void Start()
    {
        Vector2 newSize;
        newSize.x = slotSize.x * gridSize.x;
        newSize.y = slotSize.y * gridSize.y;
        inventoryContent.sizeDelta = newSize;
    }

    public bool Add(Item item)// añado el item 
    {
        Tuple<int, int> availableCoord;
        if (AreSlotsAvailables(item, out availableCoord))
        {
            InventoryItem inventoryItem = Instantiate(inventoryItemPrefab, inventoryContent);
            items.Add(inventoryItem);
            print("item add: " + items.Count);
            inventoryItem.Initislize(item, this);
            inventoryItem.UpdateSize(slotSize);
            //con iñaki
            //AddToGrid(inventoryItem, availableCoord.Item1, availableCoord.Item1); 
            AddToGrid(inventoryItem, availableCoord.Item1, availableCoord.Item2);
            return true;
        }
        return false;
    }

    private bool AreSlotsAvailables(Item item, out Tuple<int, int> coord)// verifico si el slot esta disponible
    {
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {

                if (ItemCanEnter(item, x, y))
                {
                    coord = new Tuple<int, int>(x, y);
                    return true;
                }
            }
        }
        coord = null;
        return false;
    }

    private bool ItemCanEnter(Item item, int slotX, int slotY) // verifico si puedo entrar
    {
        int xLimit = slotX + item.Large - 1;
        int yLimit = slotY + item.Height - 1;
        int gridLimitX = grid.GetLength(0);
        int gridLimitY = grid.GetLength(1);

        if (xLimit < gridLimitX && yLimit < gridLimitY)
        {
            //con iñaki
            //for (int x = slotX; x < gridLimitX; x++)
            //{
            //    for (int y = slotY; y < gridLimitY; y++)
            //    {
            //        if (grid[x, y] != null)
            //        {
            //            return false;
            //        }
            //    }
            //}
            for (int x = slotX; x <= xLimit; x++)
            {
                for (int y = slotY; y <= yLimit; y++)
                {
                    if (grid[x, y] != null)
                    {
                        print("No puede entrar");
                        return false;
                    }
                }
            }
            print($"RETORNA TRUE, xLimit = {xLimit}, el gridlimitX = {gridLimitX}, yLimit = {yLimit}, el gridlimitY = {gridLimitY}");
            return true;
        }
        return false;
    }

    private void AddToGrid(InventoryItem item, int slotX, int slotY)// añado el item a la grid 
    {
        // int gridLimitX = grid.GetLength(0);
        //int gridLimitY = grid.GetLength(1);

        for (int x = 0; x < item.GridSpace; x++)
        {
            for (int y = 0; y < item.GridVolum; y++)
            {
                grid[x + slotX, y + slotY] = item;
            }
        }

        float positionX = slotSize.x * slotX;
        float positionY = -slotSize.y * slotY;
        print($"SAVING IN SLOT {slotX},{slotY}");
        Vector2 position = new Vector2(positionX, positionY);
        print($"Item position set to: {position}");
        item.SetPosition(position);
    }

    public void Remove(Item item) // remuevo el item de la grid 
    {
        print("entras en el remuve1: " + items.Count);
        int i = 0;
        foreach (InventoryItem inventoryItem in items)
        {
            if (inventoryItem.Item == item)
            {
                print($"se remueve el objeto{item}");
                Vector3 dropPosition = GetPlayerFrontPosition();
                GameObject droppedObject = Instantiate(item.ObjetoReferencia1, dropPosition, Quaternion.identity);
                Relocator relocator = droppedObject.GetComponent<Relocator>();
                if (relocator)
                {
                    relocator.SetAtFloor(dropPosition);
                }
                items.RemoveAt(i);
                Destroy(inventoryItem.gameObject);
                break;
            }
            i++;
            print(i);
        }
        print("entras en el remuve2: " + items.Count);
        //items.Remove(item);
    }
    public void EnableItemsRemove()// habilito el boton de remover
    {
        if (EnableRemove.isOn)
        {
            foreach (Transform item in inventoryContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform item in inventoryContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }
        }
    }
    private Vector3 GetPlayerFrontPosition()
    {

        if (player != null)
        {
            Transform playerTransform = player.transform;
            Vector3 forwardPosition = playerTransform.position + playerTransform.forward * 1f; // Ajusta la distancia según sea necesario
            return forwardPosition;
        }

        else
        {
            Debug.LogWarning("No se encontró un objeto con el tag 'Player'.");
            return Vector3.zero; // En caso de error, retorna la posición (0,0,0)
        }
    }
    //private Vector3 GetPlayerHandPosition()
    //{
    //    return hand.position;
    //}
    public void ChangeToHand(Item item)
    {
        #region Intento de mover el objeto a la mano 1
        //int i = 0;
        //foreach (InventoryItem inventoryItem in items)
        //{
        //    if (inventoryItem.Item == item)
        //    {
        //        Debug.Log($"Removiendo el objeto del inventario: {item}");

        //        // Obtén la posición de la mano
        //        Vector3 itemPosition = GetPlayerHandPosition();

        //        // Obtén el componente `ItemPickUp` y verifica que existe
        //        ItemPickUp itemPickUp = item.ObjetoReferencia1.GetComponent<ItemPickUp>();
        //        if (itemPickUp == null)
        //        {
        //            Debug.LogError("El objeto no tiene un componente ItemPickUp.");
        //            return;
        //        }

        //        // Obtén el componente `Input_Controller` y verifica que existe
        //        Input_Controller inputController = Player.GetComponent<Input_Controller>();
        //        if (inputController == null)
        //        {
        //            Debug.LogError("No se encontró el componente Input_Controller en el jugador.");
        //            return;
        //        }

        //        // Coloca el objeto en la mano usando el método de `Input_Controller`
        //        inputController.InteractItemHand(itemPickUp);

        //        // Remueve el objeto del inventario
        //        items.RemoveAt(i);
        //        Destroy(inventoryItem.gameObject);

        //        Debug.Log("El objeto fue movido a la mano.");
        //        return;
        //    }
        //    i++;
        //}
        //Debug.LogWarning("El objeto no se encontró en el inventario.");


        //int i = 0;
        //foreach (InventoryItem inventoryItem in items)
        //{
        //    if (inventoryItem.Item == item)
        //    {
        //        print($"se remueve el objeto{item}");

        //        Vector3 itemVaMano = GetPlayerHandPosition();
        //        GameObject drop = item.ObjetoReferencia1;
        //        ItemPickUp droppedObject = drop.GetComponent<ItemPickUp>();
        //        drop.transform.position = itemVaMano;
        //        droppedObject.ChangeTypeItem(1);
        //        Input_Controller obj = Player.GetComponent<Input_Controller>();
        //        obj.InteractItemHand(droppedObject);
        //        droppedObject.transform.SetParent(Hand);
        //        //Rigidbody rig = droppedObject.AddComponent<Rigidbody>();
        //        //rig.useGravity = false;
        //        items.RemoveAt(i);
        //        Destroy(inventoryItem.gameObject);
        //        break;
        //    }
        //    i++;
        //}
        #endregion

        Vector3 dropPosition = GetPlayerFrontPosition();
        GameObject droppedObject = Instantiate(item.ObjetoReferencia1, Vector3.zero, Quaternion.identity);
        ItemPickUp itemPickUp = droppedObject.GetComponent<ItemPickUp>();
        if (itemPickUp)
        {
            if (inputController.PutItemInHand(itemPickUp))
            {
                int i = 0;
                foreach (InventoryItem inventoryItem in items)
                {
                    if (inventoryItem.Item == item)
                    {
                        print($"se remueve el objeto{item} tras ponerlo en la mano");
                        items.RemoveAt(i);
                        Destroy(inventoryItem.gameObject);
                        break;
                    }
                    i++;
                }
            }

        }
        else
        {
            Destroy(droppedObject);
        }
    }

    //public void ListItems()
    //{
    //    foreach (Item item in items)
    //    {
    //        GameObject obj = Instantiate(InventoryItem, ItemContent);
    //        var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
    //        var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
    //        itemName.text = item.itemName;
    //        itemIcon.sprite = item.icon;
    //    }
    //}

    //public void ListItems()
    //{
    //    //limpiar contenido 
    //    foreach (Transform item in inventoryContent)
    //    {
    //        Destroy(item.gameObject);
    //    }

    //    //almacenar contenido
    //    foreach (Item item in items)
    //    {
    //        GameObject obj = Instantiate(InventoryItem, inventoryContent);

    //        // Encuentra los componentes de UI en el prefab instanciado

    //        var itemName = obj.transform.Find("ItemName")?.GetComponent<Text>();
    //        if (itemName != null)// este if es un parche anti el fallo
    //        {
    //            if (itemName == null)
    //            {
    //                Debug.Log("es el nombre");
    //            }
    //            var itemIcon = obj.transform.Find("ItemIcon")?.GetComponent<Image>();
    //            if (itemIcon == null)
    //            {
    //                Debug.Log("es el nombre");
    //            }
    //            // Verificación de null
    //            if (itemName == null || itemIcon == null)
    //            {
    //                Debug.LogError("El prefab InventoryItem no tiene los componentes necesarios (Text e Image) o están mal nombrados.");
    //                continue; // Salta este item si falta algún componente
    //            }

    //            // Asigna el texto y la imagen
    //            itemName.text = item.itemName;
    //            itemIcon.sprite = item.icon;
    //        }
    //    }
    //}
}
