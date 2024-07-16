using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using TMPro;

public class TextBehaviour : MonoBehaviour
{
    public TextMeshProUGUI txt;
    
    public void setText(float value)
    {
        txt.text = value.ToString();
    }
    
    public void setStatText(float value, float maxValue)
    {
        txt.text = value.ToString() + " / " + maxValue.ToString();
    }
}
