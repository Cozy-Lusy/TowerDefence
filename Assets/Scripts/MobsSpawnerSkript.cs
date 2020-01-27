using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobsSpawnerSkript : MonoBehaviour
{
    public GameObject EnemyObject;
    public GameObject hpPrefab;
    public GameObject canvas;

    public float spawnTime;
    public float enemyInterval;
    public int waveSize;

    public Transform[] wayPoints;

    int enemyCount = 0;

    void Start()
    {
        InvokeRepeating("SpawnWave", spawnTime, enemyInterval); //Вызвать SpawnWave через spawnTime времени после начала игры с интервалом enemyInterval.
    }

    void Update()
    {
        if (enemyCount == waveSize)
        {
            CancelInvoke("SpawnWave");
        }
    }

    void SpawnWave()
    {
        enemyCount++;
        GameObject enemy = Instantiate(EnemyObject, transform.position, Quaternion.identity) as GameObject; //Спавним врагов

        enemy.GetComponent<EnemySkript>().waypoints = wayPoints; //Враги последовательно двигаются по точкам

        GameObject hp = Instantiate(hpPrefab, Vector3.zero, Quaternion.identity); //Создаем HP врагу
        hp.transform.SetParent(canvas.transform); //Назначаем нашему HP родителя в виде canvas
        hp.GetComponent<HpEnemySkript>().enemy = enemy; 

        enemy.GetComponent<EnemySkript>().hpEnemy = hp;
    }
}

//Изменить способ старта новой волны врагов, на данный момент реализована только одна волна.
