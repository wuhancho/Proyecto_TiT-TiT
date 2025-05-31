using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalJuengo : MonoBehaviour
{
    [SerializeField] private VideoSceneChanger videoSceneChanger;
    private void OnTriggerEnter(Collider other)
    {
        videoSceneChanger.ChangeSceneEspecifivic(3);
    }
}
