using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public EnemyMovement[] enemies;
    public BoostBehaviour[] boosts;


    public GameObject gameOverPanel;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemies(2, enemies[0])); //Spawns normal slime
        StartCoroutine(spawnEnemies(10, enemies[1])); //Spawns speed slime
        StartCoroutine(spawnEnemies(30, enemies[2])); //Spawns tough slime
        StartCoroutine(spawnEnemies(90, enemies[3])); //Spawns elite slime
        StartCoroutine(spawnEnemies(600, enemies[4])); //Spawns boss slime

        StartCoroutine(spawnBoost(5, boosts[0]));
        StartCoroutine(spawnBoost(7, boosts[1]));
        StartCoroutine(spawnBoost(9, boosts[2]));
        StartCoroutine(spawnBoost(11, boosts[3]));
        StartCoroutine(spawnBoost(13, boosts[4]));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawnEnemies(int timer, EnemyMovement enemyType)
    {
        float bound_x = 40f;
        float bound_y = 36f;

        while (true)
        {
            yield return new WaitForSeconds(timer);
            Instantiate(enemyType, spawnPosition(bound_x, bound_y), Quaternion.identity);
        }

    }

    IEnumerator spawnBoost(int timer, BoostBehaviour boostType)
    {
        float bound_x = 40f;
        float bound_y = 36f;

        while (true)
        {
            yield return new WaitForSeconds(timer);
            Instantiate(boostType, spawnPosition(bound_x, bound_y), Quaternion.identity);
        }
    }

    public Vector2 spawnPosition(float x, float y)
    {
        return new Vector2(Random.Range(-x, x), Random.Range(-y, y));
    }

    public void gameOver()
    {
        Debug.Log("Game over");
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }
}
