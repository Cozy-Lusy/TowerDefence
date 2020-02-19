using UnityEngine;

public class EnemyScript : MonoBehaviour {
    
    [Header("Set in Inspector")]
    public float Speed;

    [Header("Set Dynamically")]
    public GameObject HpEnemy;
    public Transform[] Waypoints;
    public Transform Target;

    private int _waypointIndex = 0;

    void Update () 
	{
        //Движение врагов по точкам
        if (_waypointIndex < Waypoints.Length){ 
	        transform.position = Vector3.MoveTowards(transform.position, Waypoints[_waypointIndex].position, Time.deltaTime * Speed); //Двигаться по направлению к точке пути
            
            transform.LookAt(Waypoints[_waypointIndex].position); //Смотреть по направлению движения
	
	        if(Vector3.Distance(transform.position, Waypoints[_waypointIndex].position) < 0.5f) //На определенной дистанции меняем точку пути на следующую
	        {
		        _waypointIndex++;
	        }
        }
        
        //Если HP врага на нуле уничтожаем и врага, и жизни
        if (HpEnemy.GetComponent<HpEnemyScript>().HpCount <= 0)
        {
            Destroy(gameObject);
            Destroy(HpEnemy);
        }
    }
}

