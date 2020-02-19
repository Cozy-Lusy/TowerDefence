using UnityEngine;
using TMPro;

public class HpEnemyScript : MonoBehaviour
{
    [Header("Set in Inspector")]
    public int HpCount = 30;

    [Header("Set Dynamically")]
    public GameObject Enemy;

    public void Damage(int countDmg)
    {
        HpCount -= countDmg;
    }

    void Update()
    {
        //Количество жизней врага располагаем в той же позиции с ним
        GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(Enemy.transform.position);
        GetComponent<TextMeshProUGUI>().text = HpCount.ToString();
    }
}
