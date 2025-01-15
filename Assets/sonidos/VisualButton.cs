using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class VisualButton : MonoBehaviour
{
    Button self_;
    ColorBlock normalColor_;
    [SerializeField] VisualButton[] otherButtons_;
    void Start()
    {
        self_ = gameObject.GetComponent<Button>();
        if (self_ != null ) { 
            normalColor_ = self_.colors;
            if (self_.name == "SetBoy") { Selected(); }
        }
    }

    public void Selected()
    {
        if (self_ != null && normalColor_ != null)
        {
            ColorBlock selectedColor = normalColor_;
            selectedColor.normalColor = self_.colors.selectedColor;
            self_.colors = selectedColor;
            if (otherButtons_ != null)
            {
                for (int i = 0; i < otherButtons_.Length; i++)
                { otherButtons_[i].Reset(); }
            }
        }
    }

    public void Reset()
    {
        if (self_ != null && normalColor_ != null)
        {
            self_.colors = normalColor_;
        }
    }

}
