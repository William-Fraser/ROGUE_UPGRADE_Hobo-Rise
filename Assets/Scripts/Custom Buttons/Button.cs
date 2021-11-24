using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

public class Button : UnityEngine.UI.Button
{
    protected override void Awake()
    {
        if (this.gameObject.GetComponent<CustomButtonMonoBehaviour>() == null)
        {
            CustomButtonMonoBehaviour mono = gameObject.AddComponent<CustomButtonMonoBehaviour>();
            mono.enabled = true;
            mono.SetButton(this);
        } else
        {
            this.gameObject.GetComponent<CustomButtonMonoBehaviour>().enabled = true;
            this.gameObject.GetComponent<CustomButtonMonoBehaviour>().SetButton(this);
        }
    }
    public void Disable()
    {
        if (enabled == false) return;
        this.enabled = false;
    }
    public void Enable()
    {
        if (enabled == true) return;
        this.enabled = true;
    }
}
