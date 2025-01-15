using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayButton : MonoBehaviour
{
    Button playButton_;
    private void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        playButton_ = gameObject.GetComponent<Button>();
    }

    private void Update()
    {
        //if (playButton_ != null)
        //{
        //    if (DataManage.DataManager.IsRecordSelected())
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