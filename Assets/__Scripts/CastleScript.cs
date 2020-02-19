using UnityEngine;
using TMPro;

public class CastleScript : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] private TextMeshProUGUI _hpCastlePrefab = null;
    public int HpCastle = 100;
    public int Damage = 10;

    private void Update()
    {
        _hpCastlePrefab.text = HpCastle.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Если враг доходит до нашего замка вычитаем damage из жизней замка и уничтожаем врагов
        if (other.gameObject.GetComponent<EnemyScript>() != null)
        {
            HpCastle -= Damage; 
            Destroy(other.gameObject);
            Destroy(other.GetComponent<EnemyScript>().HpEnemy);
        }
    }
}

//Добавить проверку если HP замка ниже или равно 0, то заканчивать игру.