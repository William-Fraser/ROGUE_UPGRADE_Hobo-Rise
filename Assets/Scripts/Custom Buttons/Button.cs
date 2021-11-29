using UnityEngine;
using UnityEngine.UI;

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
    public void Disable(Color color)
    {
        if (enabled == false) return;
        this.enabled = false;
        this.gameObject.GetComponent<Image>().color = color;
    }
    public void Enable(Color color)
    {
        if (enabled == true) return;
        this.enabled = true;
        this.gameObject.GetComponent<Image>().color = color;
    }
}
