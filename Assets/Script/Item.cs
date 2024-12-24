using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="New Item", menuName ="Item/Create New Item")]
public class Item : ScriptableObject
{
    [SerializeField] private int id, sizeX,sizeY;
    public string itemName;
    public Sprite icon;
    [SerializeField] private GameObject ObjetoReferencia;


    public int Large => sizeX;
    public int Height => sizeY;

    public GameObject ObjetoReferencia1 { get => ObjetoReferencia; set => ObjetoReferencia = value; }
}
