using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;

    [SerializeField] private Image iconImage;
    private Item item;
    [SerializeField] private GameObject boton;
    private InventoryManager inventoryManager;
    //private void Awake()
    //{
    //    inventoryManager = GetComponent<InventoryManager>();
    //}

    public Item Item { get => item; }
    public int GridSpace => item.Large;
    public int GridVolum => item.Height;

    public void Initislize(Item item, InventoryManager inventoryManager)
    {
        this.item = item;
        nameText.text = item.itemName;
        iconImage.sprite = item.icon;
        this.inventoryManager = inventoryManager;

    }

    internal void SetPosition(Vector2 position)
    {
        ((RectTransform)transform).anchoredPosition = position;
    }

    public void UpdateSize(Vector2 slotSize)
    {
        RectTransform rectTransform = ((RectTransform)transform);
        slotSize.x *= item.Large;
        slotSize.y *= item.Height;
        rectTransform.sizeDelta = slotSize;
    }

    public void RemoveItem()
    {

        print($"entras en el boton del item:{item}");
        inventoryManager.Remove(item);
    }
}