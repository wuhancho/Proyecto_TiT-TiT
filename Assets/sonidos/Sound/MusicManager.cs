/**
 * Creanavarra
 * Grado en diseño de videojuegos 
 * 2º. Asignatura: Proyecto
 * 2022 -2023
 * 
 * 
 * @File RecrdButton.cs
 * @brief class RecrdButton
 *
 *         Contains: 
 *               List with musics to be played (0 menu, 1 history, 2 romantic, 3 shop)
 *         Can do:
 *         
 *  @devs volume should be controlled from a general audio mixer, but since I have, ultimately, 
 *  not added any further sounds than backgound music I just controlled its volume throuh here.
 *  
 *         references 
 *              https://www.youtube.com/watch?v=DU7cgVsU2rM 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; 

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer; 
    [SerializeField] private List<AudioSource> musicList_;
    private AudioSource currentSource_;
    private void play()
    {
        currentSource_.loop = true;
        currentSource_.Play();
    }
    private void stop()
    {
        currentSource_.Pause();
    }
    public void playMenu(bool first = false) {
        if (musicList_ != null)
        {
            if (musicList_.Count >= 1)
            {
                if (currentSource_ == null)
                {
                    currentSource_ = musicList_[0];
                }
                else if (currentSource_.gameObject.name != musicList_[0].name)
                {
                    stop();
                    currentSource_ = musicList_[0];
                }
                if (!currentSource_.isPlaying) { play(); }
            }
        }
    }
    public void playGame(int n) {
        if (musicList_ != null)
        {
            if (musicList_.Count >= n +1)
            {
                if (currentSource_ == null)
                {
                    currentSource_ = musicList_[n];
                }
                else if (currentSource_.gameObject.name != musicList_[n].name)
                {
                    stop();
                    currentSource_ = musicList_[n];
                }
                if (!currentSource_.isPlaying) { play(); }
            }
        }
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
        
    }
}
