using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarBehaviour : MonoBehaviour
{
    public Slider bar;
    
    public void setMaxBar(float maxValue)
    {
        bar.maxValue = maxValue;
        bar.value = maxValue;
    }
    public void setNewMaxBar(float maxValue)
    {
        bar.maxValue = maxValue;
    }
    public void setBar(float value)
    {
        bar.value = value;
    }
}
