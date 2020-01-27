using UnityEngine;
using System.Collections;

public class TowerSkript : MonoBehaviour {

    public Transform shootElement;
    public Transform gunPivot;
    public Transform enemyTarget;

    public GameObject bulletPrefab;
    public GameObject curTarget;

    public bool isFindTarget;
    public int damage = 10;
    public float shootDelay;

    bool isShoot;

    void Update () {
        // Поворот пушки в сторону вражеской цели
        if (enemyTarget)
        {  
            Vector3 dir = enemyTarget.transform.position - gunPivot.transform.position;

            Quaternion rotation = Quaternion.LookRotation(dir);                
            gunPivot.transform.rotation = Quaternion.Slerp(gunPivot.transform.rotation, rotation, 5 * Time.deltaTime);
        }
        // Поочередные выстрелы с помощью IEnumerator shoot()
        if (!isShoot)
        {
            StartCoroutine(shoot());
        }
    }

	IEnumerator shoot()
	{
		isShoot = true;
		yield return new WaitForSeconds(shootDelay); //Новый выстрел через каждые shootDelay

        if (enemyTarget)
        {
            GameObject bullet = Instantiate(bulletPrefab, shootElement.position, Quaternion.identity);
            bullet.GetComponent<TowerBulletSkript>().target = enemyTarget;
            bullet.GetComponent<TowerBulletSkript>().tower = this;
        }

        isShoot = false; //Выстрел произведен, значит можно стрелять заново
	}
    void OnTriggerEnter(Collider other)
    {
        //Если враг находится в зоне башни и не имеет цели, то устанавливаем цель
        if (other.CompareTag("Enemy") && !isFindTarget)
        {
            enemyTarget = other.gameObject.transform;
            curTarget = other.gameObject;
            isFindTarget = true;
        }

        //Если враг вошел в зону, но не вышел потому что умер :с
        if (curTarget == null)
        {
            isFindTarget = false;
            enemyTarget = null;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Если враг, который является целью башни, вышел из зоны поражения, то убираем цель
        if (other.CompareTag("Enemy") && other.gameObject == curTarget)
        {
            isFindTarget = false;
            enemyTarget = null;
        }
    }
}

//Продумать поиск врагов эффективнее, без использования collider'а проверять есть ли враги в определенном радиусе от башни.
