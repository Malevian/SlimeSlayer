using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    private float speed = 4.5f;

    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;

        outOfBounds();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")){
            Destroy(gameObject);
            EnemyMovement enemy = collision.gameObject.GetComponent<EnemyMovement>();
            enemy.TakeDamage(player.player.Attack);
        }
    }

    private void outOfBounds()
    {
        float bound_x = 50f, bound_y = -50f;

        if (transform.position.x < bound_x && transform.position.x > -bound_x) return;

        if (transform.position.y < bound_y || transform.position.y > -bound_y) return;

        Destroy(gameObject);
    }
}
