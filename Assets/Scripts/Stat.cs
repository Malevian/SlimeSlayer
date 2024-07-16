using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    private Image content;

    public float maxValue {  get; set; }
    private float curValue { get { return curValue; } set { curValue = curValue > maxValue ? maxValue: value; } }

    // Start is called before the first frame update
    void Start()
    {
        content = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
