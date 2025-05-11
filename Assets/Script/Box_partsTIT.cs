using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Box_partsTIT : MonoBehaviour
{
    [SerializeField] private ItemPickUp PartTiTprefab;
    public ItemPickUp OnRaycastHit()
    {
        ItemPickUp instance = Instantiate(PartTiTprefab, transform.position, Quaternion.identity);
        return instance;
    }
}
