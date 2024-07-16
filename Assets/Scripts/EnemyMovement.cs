using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] public Unit enemy = new Unit(10f, 1, 0, 1);

    [SerializeField] int exp = 1;
    Rigidbody2D rb;
    Transform target;
    Vector2 movementDirection;

    private PlayerController player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            movementDirection = (target.position - transform.position).normalized;
        }
    }
    private void FixedUpdate()
    {
        if (target)
        {
            rb.velocity = new Vector2(movementDirection.x, movementDirection.y) * enemy.Speed * 5;
        }
    }
    public void TakeDamage(float damageAmount)
    {
        enemy.Health -= damageAmount;

        if(enemy.Health <= 0)
        {
            player.Experience += exp;
            Destroy(gameObject);
        }
    }
}
