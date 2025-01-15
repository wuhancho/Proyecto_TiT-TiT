/**
 * Creanavarra
 * Grado en diseño de videojuegos 
 * 2º. Asignatura: Proyecto
 * 2022 -2023
 * 
 * 
 * @file ButtonPlaySound.cs 
 * @brief class ButtonHideUnhideObject
 * 
 *      Actives or deactivates a object depending on wether it is 
 *      the first or second click made over the button
 *     
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHideUnhideObject : MonoBehaviour
{
    [SerializeField] GameObject showHideObject_;
    bool active_;

    private void Awake()
    {
        if (showHideObject_ != null)
        {
            active_ = showHideObject_.activeInHierarchy;
        }
    }

    public void ShowHide()
    {
        active_ = !active_;
        showHideObject_.SetActive(active_); 
    }
}
