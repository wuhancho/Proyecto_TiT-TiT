/* 
 * Creanavarra
 * Grado en diseño de videojuegos 
 * 2º. Asignatura: Proyecto
 * 2022 -2023
 * 
 * @file VFXManager.cs 
 * @brief class VFXManager
 * 
 *      Its objective is to be used to display any punctual sound 
 *      at any given time of the game
 *      
 *      To do so I've followed the advices and steps indicated by the following tutorial: 
 *       https://www.youtube.com/watch?v=DU7cgVsU2rM 
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager vfxInstance_;
    [SerializeField] AudioSource vfxPreFab_;
    public float volume_ = 0.7f;
    private void Awake()
    {
        if (vfxInstance_ == null) { vfxInstance_ = this;  }
    }

    public void playVFX(AudioClip vfx, Transform spawnTransform)
    { 
        if (vfx != null)
        {
            AudioSource[] playingSounds = null;
            if (spawnTransform.parent != null)
            {
                if (spawnTransform.parent.parent != null)
                {
                    playingSounds = spawnTransform.parent.parent.GetComponentsInChildren<AudioSource>();
                }
                else { playingSounds = spawnTransform.parent.GetComponentsInChildren<AudioSource>(); }
            }
            else if (spawnTransform != null)
            { playingSounds = spawnTransform.GetComponentsInChildren<AudioSource>(); }
            AudioSource audioSource;

            if (playingSounds != null)
            {
                if (playingSounds.Length > 0)
                {
                    audioSource = playingSounds[0];
                    audioSource.Stop();
                    for (int i = 1; i < playingSounds.Length; i++)
                    {
                        if (playingSounds[i] != null)
                        {
                            playingSounds[i].Stop();
                            Destroy(playingSounds[i]);
                        }
                    }
                }
                else
                { audioSource = Instantiate(vfxPreFab_, spawnTransform); }
            }
            else
            { audioSource = Instantiate(vfxPreFab_, spawnTransform); }

            audioSource.clip = vfx;
            audioSource.volume = volume_;
            audioSource.Play();
        }
    }
}