using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostBehaviour : MonoBehaviour
{
    public BoostType boostType;
    public float value;

    private Vector2 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        UpAndDownAnimation();
    }

    void UpAndDownAnimation()
    {
        float amplitude = 0.5f;
        float speed = 2;
        float newY = startPos.y + amplitude * Mathf.Sin(speed * Time.time);

        transform.position = new Vector2(transform.position.x, newY);
    }
}
