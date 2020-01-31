using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
    
    public float Speed;

    int waypointIndex = 0;

    public Transform[] waypoints;
    public Transform target;

    public GameObject hpEnemy;

    void Update () 
	{
        //Движение врагов по точкам
        if (waypointIndex < waypoints.Length){ 
	        transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].position, Time.deltaTime * Speed); //Двигаться по направлению к точке пути
            
            transform.LookAt(waypoints[waypointIndex].position); //Смотреть по направлению движения
	
	        if(Vector3.Distance(transform.position, waypoints[waypointIndex].position) < 0.5f) //На определенной дистанции меняем точку пути на следующую
	        {
		        waypointIndex++;
	        }
        }
        
        //Если HP врага на нуле уничтожаем и врага, и жизни
        if (hpEnemy.GetComponent<HpEnemyScript>().hp <= 0)
        {
            Destroy(gameObject);
            Destroy(hpEnemy);
        }
    }
}

