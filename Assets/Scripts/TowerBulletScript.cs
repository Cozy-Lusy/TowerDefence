using UnityEngine;
using System.Collections;

public class TowerBulletScript : MonoBehaviour {

    public float Speed;

    public Transform target;

    public GameObject explosionBullet;

    public TowerScript tower;

    void Update() {

        // Стреляем в направлении врага
        if (target) 
        {        
            transform.LookAt(target);
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * Speed); 
        } 
    }
    void OnTriggerEnter(Collider other)
    {
        //Если цель действительно враг уничтожаем ядро при столкновении с врагом
        if (other.gameObject.transform == target) 
        {
            target.GetComponent<EnemyScript>().hpEnemy.GetComponent<HpEnemyScript>().Damage(tower.damage);

            Destroy(gameObject, 0.05f);

            //Добавляем анимацию взрыва
            explosionBullet = Instantiate(explosionBullet, transform.position, Quaternion.identity);
            explosionBullet.transform.parent = target.transform;
            Destroy(explosionBullet, 1);
        }
    }
}
