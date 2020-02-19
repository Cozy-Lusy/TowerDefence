using UnityEngine;
using System.Collections;

public class TowerScript : MonoBehaviour {

    [Header("Set in Inspector")]
    [SerializeField] private Transform _shootElement = null;
    [SerializeField] private Transform _gunPivot = null;
    [SerializeField] private GameObject _bulletPrefab = null;
    public bool IsFindTarget;
    public int Damage = 10;
    public float ShootDelay = 1f;

    [Header("Set Dynamically")]
    public Transform EnemyTarget;
    public GameObject CurTarget;

    private bool _isShoot;

    private void Update () {
        // Поворот пушки в сторону вражеской цели
        if (EnemyTarget)
        {  
            Vector3 dir = EnemyTarget.transform.position - _gunPivot.transform.position;

            Quaternion rotation = Quaternion.LookRotation(dir);                
            _gunPivot.transform.rotation = Quaternion.Slerp(_gunPivot.transform.rotation, rotation, 5 * Time.deltaTime);
        }
        // Поочередные выстрелы с помощью IEnumerator shoot()
        if (!_isShoot)
        {
            StartCoroutine(shoot());
        }
    }

	private IEnumerator shoot()
	{
		_isShoot = true;
		yield return new WaitForSeconds(ShootDelay); //Новый выстрел через каждые ShootDelay

        if (EnemyTarget)
        {
            GameObject bullet = Instantiate(_bulletPrefab, _shootElement.position, Quaternion.identity);
            bullet.GetComponent<TowerBulletScript>().Target = EnemyTarget;
            bullet.GetComponent<TowerBulletScript>().Tower = this;
        }

        _isShoot = false; //Выстрел произведен, значит можно стрелять заново
	}
    private void OnTriggerEnter(Collider other)
    {
        //Если враг находится в зоне башни и не имеет цели, то устанавливаем цель
        if (other.gameObject.GetComponent<EnemyScript>() && !IsFindTarget)
        {
            EnemyTarget = other.gameObject.transform;
            CurTarget = other.gameObject;
            IsFindTarget = true;
        }

        //Если враг вошел в зону, но не вышел потому что умер :с
        if (CurTarget == null)
        {
            IsFindTarget = false;
            EnemyTarget = null;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Если враг, который является целью башни, вышел из зоны поражения, то убираем цель
        if (other.gameObject.GetComponent<EnemyScript>() && other.gameObject == CurTarget)
        {
            IsFindTarget = false;
            EnemyTarget = null;
        }
    }
}

//Продумать поиск врагов эффективнее, без использования collider'а проверять есть ли враги в определенном радиусе от башни.
