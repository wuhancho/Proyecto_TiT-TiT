using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadButton : MonoBehaviour
{
    Button playButton_;

    public void Initialize()
    {
        playButton_ = gameObject.GetComponent<Button>();
    }

    public void CheckIfActive()
    {
        //if (playButton_ != null)
        //{
        //    if (DataManage.DataManager.AvailableRecords())
        //    {
        //        playButton_.interactable = true;
        //    }
        //    else
        //    {
        //        playButton_.interactable = false;
        //    }
        //}
    }
}
