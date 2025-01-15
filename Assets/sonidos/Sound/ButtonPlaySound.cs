/**
 * Creanavarra
 * Grado en diseño de videojuegos 
 * 2º. Asignatura: Proyecto
 * 2022 -2023
 * 
 * 
 * @file ButtonPlaySound.cs 
 * @brief class ButtonPlaySound
 * 
 *         Plays the assigned sound when a button is pressed
 *
 * @devs Can be modified to add a different sound for hover 
 *     
 */

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonPlaySound : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] AudioClip vfx_;
    [SerializeField] AudioClip hoverVfx_;
    [SerializeField] GameObject Context_;
    Button button_;

    private void Awake()
    {
        button_ = GetComponent<Button>();
        if (button_ != null)
        {
            button_.interactable = true;
            button_.onClick.AddListener(() => OnClickVFX());
        }
    }
    private void playSound(AudioClip audio)
    {
        if (Context_ != null)
        {
            VFXManager.vfxInstance_.playVFX(audio, Context_.transform);
        }
        else
        {
            VFXManager.vfxInstance_.playVFX(audio, gameObject.transform);
        }
    }
    public void OnClickVFX()
    {
        playSound(vfx_);
    }
    public void OnHoverkVFX()
    {
        playSound(hoverVfx_);
    }
    
   public void OnPointerEnter(PointerEventData pointerEvent) { OnHoverkVFX(); }
   
  
}
