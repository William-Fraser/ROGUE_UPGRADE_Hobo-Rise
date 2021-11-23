using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

public class Button : UnityEngine.UI.Button
{
    protected override void Awake()
    {
        if (this.gameObject.GetComponent<CustomButtonMono>() == null)
        {
            CustomButtonMono mono = gameObject.AddComponent<CustomButtonMono>();
            mono.enabled = true;
            mono.SetButton(this);
        } else
        {
            this.gameObject.GetComponent<CustomButtonMono>().enabled = true;
            this.gameObject.GetComponent<CustomButtonMono>().SetButton(this);
        }
    }
}
