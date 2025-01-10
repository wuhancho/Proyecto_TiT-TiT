using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relocator : MonoBehaviour
{

    private enum RelocateType
    {
        Collider,
        Manual
    }

    [SerializeField] private LayerMask hitLayers;
    [SerializeField] private RelocateType relocateType = RelocateType.Collider;
    [SerializeField] private Vector3 manualOffset;
    [SerializeField] private bool relocateOnStart = true;

    private void Start()
    {
        if (relocateOnStart)
        {
            SetAtFloor(transform.position);
        }
    }

    public void SetAtFloor(Vector3 position)
    {
        if (Physics.Raycast(position, Vector3.down, out RaycastHit hit, Mathf.Infinity, hitLayers))
        {
            //print(hit.collider.name);
            Set(hit.point);
        }
    }

    public void Set(Vector3 position)
    {

        switch (relocateType)
        {
            case RelocateType.Collider:
                Collider collider = GetComponent<Collider>();
                if (collider != null)
                {
                    //print($"pos: {transform.position} center: {collider.bounds.center}");
                    Vector3 offset = transform.position - collider.bounds.center;
                    //position.y -= collider.bounds.center.y;
                    position.y += collider.bounds.size.y / 2;
                    position -= offset;

                }
                break;
            case RelocateType.Manual:
                position += manualOffset;
                break;
        }

        transform.position = position;
    }
}
