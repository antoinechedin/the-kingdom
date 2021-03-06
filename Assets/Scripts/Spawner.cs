﻿using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public PathfindingManager[] pfManagers;
    public float spawnTime0;
    public float spawnTime10;

    private float timer;
    private Vector2 start;
    private Vector2 target;

    private void Awake()
    {
        start = transform.position;
        GetComponent<SpriteRenderer>().sprite = null;

        target = transform.GetChild(0).transform.position;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;

        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float spawnTime = Mathf.Lerp(spawnTime0, spawnTime10, Mathf.Min(Time.timeSinceLevelLoad / 600f, 1f)) ;
        timer += Time.deltaTime;
        if (timer > spawnTime)
        {
            timer -= spawnTime;
            SpawnMonster();
        }
    }

    private void SpawnMonster()
    {
        Monster monster = Instantiate(monsterPrefab, start, Quaternion.identity).GetComponent<Monster>();
        PathfindingManager randomManager = pfManagers[Random.Range(0, pfManagers.Length)];
        monster.pm = randomManager;
        monster.target = target;
    }
}
