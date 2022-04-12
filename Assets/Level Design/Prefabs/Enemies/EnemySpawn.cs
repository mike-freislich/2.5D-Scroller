using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

enum SpawnState { Standby, Spawning, Complete, CleanupDone }

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;
    public PathCreator pathCreatorPrefab;
    public Camera attachToCamera;
    public float pathSpeed;
    public int numberToSpawn;
    public float spawnDelay;
    public EndOfPathInstruction endOfPathInstruction;

    float spawnTimer;
    int enemyCount;
    SpawnState spawnState = SpawnState.Standby;
    PathCreator pathCreator;
    List<GameObject> enemies = new List<GameObject>();

    void Start()
    {
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        switch (spawnState)
        {
            case SpawnState.Standby: TriggerSpawn(); break;
            case SpawnState.Spawning: SpawnEnemy(); break;
            case SpawnState.Complete: CleanUp(); break;
        }
    }

    void TriggerSpawn()
    {
        if (SpawnPointOnScreen)
        {
            spawnState = SpawnState.Spawning;
            pathCreator = Instantiate(pathCreatorPrefab);
            pathCreator.transform.parent = attachToCamera.transform;
            pathCreator.transform.position = transform.position;
        }
    }

    bool SpawnPointOnScreen
    {
        get
        {
            Vector3 spawnPos = transform.position;
            Vector3 screenPoint = attachToCamera.WorldToViewportPoint(spawnPos);
            bool onScreen =
                screenPoint.z > 0 &&
                screenPoint.x > 0 && screenPoint.x < 1 &&
                screenPoint.y > 0 && screenPoint.y < 1;            
            return onScreen;
        }
    }

    void SpawnEnemy()
    {
        if (enemyCount < numberToSpawn && spawnTimer >= spawnDelay)
        {
            // Spawn Enemy
            spawnTimer = 0;
            GameObject newEnemy = Instantiate(enemy);
            newEnemy.layer = 9;
            enemies.Add(newEnemy);
            enemyCount++;

            // attach Path and set position
            PathFollower pathFollower = newEnemy.GetComponent<PathFollower>();
            if (pathFollower != null)
            {
                pathFollower.pathCreator = pathCreator;
                pathFollower.speed = pathSpeed;
                newEnemy.transform.position = pathCreator.path.GetPointAtDistance(0);
                pathFollower.endOfPathInstruction = endOfPathInstruction;
            }
        }
        if (enemyCount == numberToSpawn) spawnState = SpawnState.Complete;
    }

    void CleanUp()
    {
        int culledCount = 0;
        for (int i = 0; i < enemies.Count; i++)
            if (enemies[i] == null) culledCount++;

        if (culledCount == enemies.Count)
        {
            Destroy(pathCreator.gameObject);
            Destroy(gameObject);
            Destroy(this);
            spawnState = SpawnState.CleanupDone;
        }
    }
}
