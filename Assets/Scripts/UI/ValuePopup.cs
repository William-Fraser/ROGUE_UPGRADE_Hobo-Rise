using TMPro;
using UnityEngine;

public class ValuePopup : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private Color textColor;

    private float disapperationTimer = 0.5f;
    private float fadeSpeed = 5;
    private float yMoveSpeed = 1;

    public void Setup(string value, Color color)
    {
        //Debug.Log($"SETUP value: {value}, colour: {color}");
        Canvas canvas = GetComponentInChildren<Canvas>();
        textMesh = canvas.GetComponentInChildren<TextMeshProUGUI>();
        textColor = color;
        textMesh.color = textColor;
        textMesh.SetText(value.ToString());
    }
    public void LateUpdate()
    {
        if (textMesh != null)
        { 
            transform.position += new Vector3(0f, yMoveSpeed * Time.deltaTime, 0);

            disapperationTimer -= Time.deltaTime;

            if (disapperationTimer <= 0)
            {
                textColor.a -= fadeSpeed * Time.deltaTime;
                textMesh.color = textColor;
                if (textColor.a <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
