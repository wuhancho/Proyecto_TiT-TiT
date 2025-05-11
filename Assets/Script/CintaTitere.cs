using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CintaTitere : MonoBehaviour
{
    [SerializeField] private GameObject zone_tit_sup;
    [SerializeField] private GameObject zone_tit_inf;
    [SerializeField] private GameObject valvula;
    [SerializeField] private GameManager gameManager;
    private int cont;
    public void titActivepart(GameObject part,int sitio)
    {
        if (sitio == 1)
        {
            foreach (Transform child in zone_tit_sup.transform)
            {
                if(child.gameObject == part)
                {
                    child.gameObject.SetActive(true);
                    ComprobarPart(zone_tit_sup);
                }
            }
        }
        else if (sitio == 2)
        {
            foreach (Transform child in zone_tit_inf.transform)
            {
                if (child.gameObject == part)
                {
                    child.gameObject.SetActive(true);
                    ComprobarPart(zone_tit_inf);
                }
            }
        }
    }

    private void ComprobarPart(GameObject zona)
    {
        if (zona == zone_tit_inf)
        {
            foreach (Transform child in zona.transform)
            {
                if (child.gameObject.activeSelf)
                {
                    if (child.GetSiblingIndex() == 1)
                    {
                        CorrectPart();
                    }
                    if (child.GetSiblingIndex() == 0)
                    {
                        child.gameObject.SetActive(false);
                    }
                }
            }
        }
        else if (zona == zone_tit_sup)
        {
            foreach (Transform child in zona.transform)
            {
                if (child.gameObject.activeSelf)
                {
                    if (child.GetSiblingIndex() == 0)
                    {
                        CorrectPart();
                    }
                    if (child.GetSiblingIndex() == 1)
                    {
                        child.gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    private void CorrectPart()
    {
        cont++;
        if (cont == 2)
        {
            gameManager.Pzlemaquina();
        }
    }
}
