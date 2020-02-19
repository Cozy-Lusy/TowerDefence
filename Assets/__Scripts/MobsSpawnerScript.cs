using UnityEngine;

public class MobsSpawnerScript : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] private GameObject _enemyPrefab = null;
    [SerializeField] private GameObject _hpPrefab = null;
    [SerializeField] private GameObject _canvas = null;
    public float SpawnTime = 2;
    public float EnemyInterval = 1;
    public int WaveSize = 10;
    public Transform[] WayPoints;

    private int _enemyCount = 0;

    const string SpawnWaveName = "SpawnWave";

    private void Start()
    {
        InvokeRepeating(SpawnWaveName, SpawnTime, EnemyInterval); //Вызвать SpawnWave через SpawnTime времени после начала игры с интервалом EnemyInterval.
    }

    private void Update()
    {
        if (_enemyCount == WaveSize)
        {
            CancelInvoke(SpawnWaveName);
        }
    }

    private void SpawnWave()
    {
        _enemyCount++;
        GameObject enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity) as GameObject; //Спавним врагов

        enemy.GetComponent<EnemyScript>().Waypoints = WayPoints; //Враги последовательно двигаются по точкам

        GameObject hp = Instantiate(_hpPrefab, Vector3.zero, Quaternion.identity); //Создаем HP врагу
        hp.transform.SetParent(_canvas.transform); //Назначаем нашему HP родителя в виде canvas
        hp.GetComponent<HpEnemyScript>().Enemy = enemy; 

        enemy.GetComponent<EnemyScript>().HpEnemy = hp;
    }
}
//Изменить способ старта новой волны врагов, на данный момент реализована только одна волна.