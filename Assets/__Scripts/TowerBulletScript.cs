using UnityEngine;

public class TowerBulletScript : MonoBehaviour {

    [Header("Set in Inspector")]
    [SerializeField] private GameObject _explosionBullet = null;
    public float Speed;

    [Header("Set Dynamically")]
    public Transform Target;
    public TowerScript Tower;

    private void Update() 
    {
        // Стреляем в направлении врага
        if (Target) 
        {        
            transform.LookAt(Target);
            transform.position = Vector3.MoveTowards(transform.position, Target.position, Time.deltaTime * Speed); 
        } 
    }
    private void OnTriggerEnter(Collider other)
    {
        //Если цель действительно враг уничтожаем ядро при столкновении с врагом
        if (other.gameObject.transform == Target) 
        {
            Target.GetComponent<EnemyScript>().HpEnemy.GetComponent<HpEnemyScript>().Damage(Tower.Damage);

            Destroy(gameObject, 0.05f);

            //Добавляем анимацию взрыва
            _explosionBullet = Instantiate(_explosionBullet, transform.position, Quaternion.identity);
            _explosionBullet.transform.parent = Target.transform;
            Destroy(_explosionBullet, 1);
        }
    }
}
