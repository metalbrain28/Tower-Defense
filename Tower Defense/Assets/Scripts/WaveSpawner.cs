﻿using UnityEngine;
using System.Collections;


public class WaveSpawner : Singleton<WaveSpawner>
{

   
    public GameObject testEnemyPrefab;
    public int wavePoint;
    public Enemy[] objects;
    public Wave currentWave;
    int currentWaveNumber=0;
    public float timeLastEnemySpawned ;

    public int enemiesRemainToSpawn;
    public int enemiesRemainAlive;
    public int enemies;
    float nextSpawnTime;
     Enemy enemy;

    bool started = false;
    

   
    private int enemiesSpawned = 0;

    // Use this for initialization
    void Start()
    {
        enemies = currentWave.enemyCount;
        NextWave();
        timeLastEnemySpawned = Time.time;

    }

    public void StartEnemyWave()
    {
        enemies = currentWave.enemyCount;
        NextWave();
        timeLastEnemySpawned = Time.time;

        started = true;
    }

    void NextWave()
    {
        currentWaveNumber++;
     
        Wave newWave = Instantiate(currentWave, transform.position, Quaternion.identity) as Wave;
       
        /*
         daca ne aflam la un nivel multiplu de 5 si in primul waveSpawner
         daca inamicul e boss se va randa unul singur
         altfel numarul de inamici este cu 1 mai mare decat cel din valul anterior
         numarul inamicilor din al doilea Spawner creste cu 2
         */
        if( wavePoint==1)
        {
            enemy = objects[UnityEngine.Random.Range(0, objects.Length - 1)];
            enemy.tag = "Player";
            if (currentWaveNumber == 1 && currentWaveNumber != 0)
            {
                enemy = objects[objects.Length - 1];
            }
            if (enemy.isBoss == true)
            {
                enemies = 1;
            }
            else
            {
                enemies++;
            }

        }
        else
        {
            enemy = objects[UnityEngine.Random.Range(0, objects.Length )];
            enemies = enemies + 2;
            
        }

        currentWave.enemyCount = enemies;
           
        enemiesRemainToSpawn = currentWave.enemyCount;
        enemiesRemainAlive = enemiesRemainToSpawn;
      
    
    }
    // Update is called once per frame
    public void OnEnemyDeath()
    {
      
        enemiesRemainAlive--;
        if(enemiesRemainAlive == 0)
        {
            NextWave();
        }
    }

    void Update()
    {
        //if (!started) {
        //    return;
        //}

        if(enemiesRemainToSpawn>0 && Time.time > nextSpawnTime)
        {
            enemiesRemainToSpawn--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;

           Enemy gameObjEnm = Instantiate(enemy, transform.position, Quaternion.identity) as Enemy;
            gameObjEnm.OnDeath += OnEnemyDeath;
               
        }
    }
}
